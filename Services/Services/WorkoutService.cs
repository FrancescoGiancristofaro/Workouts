using AutoMapper;
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.Messaging;
using Repositories.Models;
using Repositories.Repositories;
using Services.Dtos;

namespace Services.Services
{
    public enum WorkoutOperation
    {
        Inserted,
        Deleted,
        Updated,
    }
    public interface IWorkoutService 
    {
        Task<IEnumerable<WorkoutsDto>> GetWorkoutsAsync();
        Task CreateWorkoutsAsync(WorkoutWizardDto dto);
    }

    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutsRepository _workoutsRepository;
        private readonly IMapper _mapper;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IWorkoutExerciseDetailsRepository _workoutExerciseDetailsRepository;
        private readonly ISeriesRepository _seriesRepository;

        public WorkoutService(
            IWorkoutsRepository workoutsRepository,
            IMapper mapper,
            IExerciseRepository exerciseRepository,
            IWorkoutExerciseDetailsRepository workoutExerciseDetailsRepository,
            ISeriesRepository seriesRepository)
        {
            _workoutsRepository = workoutsRepository;
            _mapper = mapper;
            _exerciseRepository = exerciseRepository;
            _workoutExerciseDetailsRepository = workoutExerciseDetailsRepository;
            _seriesRepository = seriesRepository;
        }

        public async Task<IEnumerable<WorkoutsDto>> GetWorkoutsAsync()
        {
            return (await _workoutsRepository.GetAll()).Select(x => _mapper.Map<WorkoutsDto>(x));
        }

        public async Task CreateWorkoutsAsync(WorkoutWizardDto dto)
        {
            var workout = new Workouts(){Description = dto.Description,Name = dto.Name};
            await _workoutsRepository.Insert(workout);

            for (var i = 0; i < dto.ExerciseList.Count; i++)
            {
                var exerciseDto = dto.ExerciseList[i];

                var ex = new WorkoutExerciseDetails()
                    { IdExercise = exerciseDto.Id.Value, IdWorkout = workout.Id.Value, Note = exerciseDto.Note };
                await _workoutExerciseDetailsRepository.Insert(ex);

                foreach (var item in dto.SeriesList[i])
                {
                    var series = _mapper.Map<Series>(item);
                    series.IdWorkout = workout.Id.Value;
                    series.IdExercise = exerciseDto.Id.Value;

                    await _seriesRepository.Insert(series);
                }

            }
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<WorkoutOperation>(WorkoutOperation.Inserted));

        }
    }
}
