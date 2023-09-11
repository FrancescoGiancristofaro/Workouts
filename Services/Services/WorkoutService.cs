using AutoMapper;
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.Messaging;
using Repositories.Models;
using Repositories.Repositories;
using Services.Dtos;
using Services.Constants;

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
        Task<WorkoutsDto> GetWorkoutByIdAsync(int id);
        Task<WorkoutSessionDto> GetWorkoutBySessionIdAsync(int id);
        Task CreateWorkoutAsync(WorkoutWizardDto dto);
        Task CreateWorkoutSessionAsync(WorkoutSessionWizardDto dto);
    }

    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutsRepository _workoutsRepository;
        private readonly IMapper _mapper;
        private readonly IExerciseService _exerciseService;
        private readonly IWorkoutExerciseDetailsRepository _workoutExerciseDetailsRepository;
        private readonly ISeriesRepository _seriesRepository;
        private readonly IWorkoutSessionRepository _workoutSessionRepository;
        private readonly ISeriesHistoryRepository _seriesHistoryRepository;
        private readonly ICacheService _cacheService;

        public WorkoutService(
            IWorkoutsRepository workoutsRepository,
            IMapper mapper,
            IExerciseService exerciseService,
            IWorkoutExerciseDetailsRepository workoutExerciseDetailsRepository,
            ISeriesRepository seriesRepository,
            IWorkoutSessionRepository workoutSessionRepository,
            ISeriesHistoryRepository seriesHistoryRepository,
            ICacheService cacheService)
        {
            _workoutsRepository = workoutsRepository;
            _mapper = mapper;
            _exerciseService = exerciseService;
            _workoutExerciseDetailsRepository = workoutExerciseDetailsRepository;
            _seriesRepository = seriesRepository;
            _workoutSessionRepository = workoutSessionRepository;
            _seriesHistoryRepository = seriesHistoryRepository;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<WorkoutsDto>> GetWorkoutsAsync()
        {
            return (await _workoutsRepository.GetAll()).Select(x => _mapper.Map<WorkoutsDto>(x));
        }

        public async Task CreateWorkoutAsync(WorkoutWizardDto dto)
        {
            var workout = new Workouts(){Description = dto.Description,Name = dto.Name};
            await _workoutsRepository.Insert(workout);

            for (var i = 0; i < dto.ExerciseList.Count; i++)
            {
                var exerciseDto = dto.ExerciseList[i];

                var ex = new WorkoutExerciseDetails()
                    { IdExercise = exerciseDto.Id.Value, IdWorkout = workout.Id.Value, Note = exerciseDto.Note,OrderInWorkout = i};
                await _workoutExerciseDetailsRepository.Insert(ex);

                foreach (var item in dto.SeriesList[i])
                {
                    var series = _mapper.Map<Series>(item);
                    series.IdWorkoutExerciseDetails = ex.Id.Value;

                    await _seriesRepository.Insert(series);
                }

            }
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<WorkoutOperation>(WorkoutOperation.Inserted));

        }

        public async Task CreateWorkoutSessionAsync(WorkoutSessionWizardDto dto)
        {
            try
            {
                var woSession = _mapper.Map<WorkoutSession>(dto);
                woSession.StartDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                woSession.EndDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                await _workoutSessionRepository.Insert(woSession);

                var opt = new ParallelOptions
                {
                    MaxDegreeOfParallelism = Environment.ProcessorCount
                };
                await Parallel.ForEachAsync(dto.SessionExercises, opt, async (ex, cancellationToken) =>
                {
                    var oldSeries = await _seriesRepository.GetSeriesByExerciseDetailId(ex.IdWorkoutExercise);

                    await Parallel.ForEachAsync(oldSeries, opt, async (series, cancellationToken) =>
                    {
                        var seriesHistory = _mapper.Map<SeriesHistory>(series);
                        seriesHistory.IdWorkoutSession = woSession.Id.Value;
                        await _seriesHistoryRepository.Insert(seriesHistory);
                        await _seriesRepository.Delete(series);
                    });

                    await Parallel.ForEachAsync(ex.Series, opt, async (series, cancellationToken) =>
                    {
                        var newSeries = _mapper.Map<Series>(series);
                        newSeries.IdWorkoutExerciseDetails = ex.IdWorkoutExercise;
                        await _seriesRepository.Insert(newSeries);
                    });
                });
            }
            finally
            {
                _cacheService.Remove(CacheKeys.WorkoutSessionProgression);
            }
        }

        public async Task<WorkoutSessionDto> GetWorkoutBySessionIdAsync(int id)
        {
            var session = await _workoutSessionRepository.GetById(id);
            return _mapper.Map<WorkoutSessionDto>(session);   
        }

        public async Task<WorkoutsDto> GetWorkoutByIdAsync(int id)
        {
            return _mapper.Map<WorkoutsDto>(await _workoutsRepository.GetById(id));

        }
    }
}
