using WorkoutsApp.Models.DB;
using WorkoutsApp.Settings;

namespace WorkoutsApp.Repositories
{
    public interface IRepository<T> where T : IAmAModel, new()
    {
        Task<IList<T>> GetAll();

    }
    public class BaseRepository<T> : IRepository<T> where T : IAmAModel, new()
    {
        protected readonly DBManager _dbManager;

        public BaseRepository(DBManager dbManager)
        {
            _dbManager = dbManager;
        }

        public async Task<IList<T>> GetAll()
        {
            await _dbManager.Init();
            return await _dbManager.Database.Table<T>().ToListAsync();
        }
    }
}
