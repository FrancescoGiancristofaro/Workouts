using WorkoutsApp.Models.DB;
using WorkoutsApp.Repositories;

namespace WorkoutsApp.Services
{
    public interface IWorkoutService
    {
        Task<IList<Workouts>> GetAllWorkouts();
    }

    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutsRepository _workoutsRepository;

        public WorkoutService(IWorkoutsRepository workoutsRepository)
        {
            _workoutsRepository = workoutsRepository;
        }


        public async Task<IList<Workouts>> GetAllWorkouts()
        {
            return await _workoutsRepository.GetAllWorkouts();
        }
    }
}
