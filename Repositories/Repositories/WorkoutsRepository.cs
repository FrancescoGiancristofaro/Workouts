using Repositories.Models;
using Repositories.Settings;

namespace Repositories.Repositories
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
