using System.Collections.Concurrent;
using AutoMapper;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Repositories.Models;
using Repositories.Repositories;
using Services.Dtos;

namespace Services.Services
{
    public enum ExerciseOperation
    {
        Inserted,
        Deleted,
        Updated,
    }
    public interface IExerciseService
    {
        Task<IEnumerable<ExerciseDto>> GetExerciseListAsync();
        Task<ExerciseDto> GetExerciseByIdAsync(int id);
        Task DeleteExerciseByIdAsync(int id);
        Task<ExerciseDto> InsertExerciseAsync(ExerciseDto dto);
        Task<IEnumerable<WorkoutExerciseDetailsDto>> GetAllExerciseDetailsByWorkoutIdAsync(int id);
        Task<IEnumerable<SeriesDto>> GetSeriesByExerciseDetailAsync(int exDetailId);
        Task<IEnumerable<SeriesHistoryDto>> GetSeriesHistoryByExerciseDetailAsync(int exDetailId);

    }

    public class ExerciseService :  IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;
        private readonly IWorkoutExerciseDetailsRepository _workoutExerciseDetailsRepository;
        private readonly ISeriesRepository _seriesRepository;
        private readonly ISeriesHistoryRepository _seriesHistoryRepository;

        public ExerciseService(
            IExerciseRepository exerciseRepository,
            IMapper mapper, 
            IWorkoutExerciseDetailsRepository workoutExerciseDetailsRepository,
            ISeriesRepository seriesRepository,
            ISeriesHistoryRepository seriesHistoryRepository)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
            _workoutExerciseDetailsRepository = workoutExerciseDetailsRepository;
            _seriesRepository = seriesRepository;
            _seriesHistoryRepository = seriesHistoryRepository;
        }

        public async Task<IEnumerable<ExerciseDto>> GetExerciseListAsync()
        {
            return (await _exerciseRepository.GetAll()).Select(x => _mapper.Map<ExerciseDto>(x));
        }

        public async Task<ExerciseDto> GetExerciseByIdAsync(int id)
        {
            var ex = await _exerciseRepository.GetById(id);
            return _mapper.Map<ExerciseDto>(ex);
        }

        public async Task DeleteExerciseByIdAsync(int id)
        {
            var ex = await _exerciseRepository.GetById(id);
            await _exerciseRepository.Delete(ex);
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<ExerciseOperation>(ExerciseOperation.Deleted));
        }

        public async Task<ExerciseDto> InsertExerciseAsync(ExerciseDto dto)
        {
            var ex = _mapper.Map<Exercise>(dto);
            var res = await _exerciseRepository.Insert(ex);
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<ExerciseOperation>(ExerciseOperation.Inserted));
            return _mapper.Map<ExerciseDto>(res);
        }

        public async Task<IEnumerable<WorkoutExerciseDetailsDto>> GetAllExerciseDetailsByWorkoutIdAsync(int id)
        {
            var exDetails = await _workoutExerciseDetailsRepository.GetByWorkoutId(id);
            return _mapper.Map<IEnumerable<WorkoutExerciseDetailsDto>>(exDetails);
        }

        public async Task<IEnumerable<SeriesDto>> GetSeriesByExerciseDetailAsync(int exDetailId)
        {
            var res = await _seriesRepository.GetSeriesByExerciseDetailId(exDetailId);
            return _mapper.Map<IEnumerable<SeriesDto>>(res);
        }

        public async Task<IEnumerable<SeriesHistoryDto>> GetSeriesHistoryByExerciseDetailAsync(int exId)
        {
            var exDetailsInWorkout = await _workoutExerciseDetailsRepository.GetAll();
            var list = new ConcurrentBag<SeriesHistory>();

            var opt = new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };

            await Parallel.ForEachAsync(exDetailsInWorkout, opt, async (item, cancellationToken) =>
            {
                var series = await _seriesHistoryRepository.GetSeriesHistoryByExerciseDetailId(item.Id.Value);
                foreach (var s in series)
                    list.Add(s);
            });

            return _mapper.Map<IEnumerable<SeriesHistoryDto>>(list);
        }
    }
}
