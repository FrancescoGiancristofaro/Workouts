using Repositories.Models;
using Repositories.Repositories;

namespace Services.Services
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
