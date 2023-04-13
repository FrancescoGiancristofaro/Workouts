using WorkoutsApp.Settings;
using WorkoutsApp.Models.DB;

namespace WorkoutsApp.Repositories
{
    public interface IWorkoutsRepository
    {
        Task<IList<Workouts>> GetAllWorkouts();

    }
    public class WorkoutsRepository : BaseRepository<Workouts>, IWorkoutsRepository
    {
        public WorkoutsRepository(DBManager dbManager) : base(dbManager)
        {
        }

        public async Task<IList<Workouts>> GetAllWorkouts()
        {
            return await base.GetAll();
        }
    }
}
