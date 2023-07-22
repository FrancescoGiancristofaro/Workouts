using AutoMapper;
using Repositories.Models;
using Repositories.Repositories;
using Services.Dtos;

namespace Services.Services
{
    public interface IWorkoutService 
    {
        Task<IEnumerable<WorkoutsDto>> GetWorkoutsAsync();
        Task CreateWorkoutsAsync(WorkoutWizardDto dto);
    }

    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutsRepository _workoutsRepository;
        private readonly IMapper _mapper;

        public WorkoutService(IWorkoutsRepository workoutsRepository,IMapper mapper)
        {
            _workoutsRepository = workoutsRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WorkoutsDto>> GetWorkoutsAsync()
        {
            return (await _workoutsRepository.GetAll()).Select(x => _mapper.Map<WorkoutsDto>(x));
        }

        public Task CreateWorkoutsAsync(WorkoutWizardDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
