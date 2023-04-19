using WorkoutsApp.Models.DB;
using WorkoutsApp.Repositories;

namespace WorkoutsApp.Services
{
    public interface IWorkoutService : IService<Workouts>
    {
    }

    public class WorkoutService : BaseService<Workouts>, IWorkoutService
    {
        private readonly IWorkoutsRepository _workoutsRepository;

        public WorkoutService(IWorkoutsRepository workoutsRepository) : base(workoutsRepository)
        {
            _workoutsRepository = workoutsRepository;
        }
    }
}
