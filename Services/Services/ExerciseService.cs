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
        
    }

    public class ExerciseService :  IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;

        public ExerciseService(IExerciseRepository exerciseRepository,IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExerciseDto>> GetExerciseListAsync()
        {
            return (await _exerciseRepository.GetAll()).Select(x => _mapper.Map<ExerciseDto>(x));
        }

        public async Task<ExerciseDto> GetExerciseByIdAsync(int id)
        {
            return _mapper.Map<ExerciseDto>(await _exerciseRepository.GetById(id));
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

    }
}
