using WorkoutsApp.Settings;
using WorkoutsApp.Models.DB;

namespace WorkoutsApp.Repositories
{
    public interface IWorkoutsRepository : IRepository<Workouts>
    {

    }
    public class WorkoutsRepository : BaseRepository<Workouts>, IWorkoutsRepository
    {
        public WorkoutsRepository(DBManager dbManager) : base(dbManager)
        {
        }
    }
}
