using Repositories.Models;
using Repositories.Settings;

namespace Repositories.Repositories
{
    public interface IRepository<T> where T : IAmAModel, new()
    {
        Task<IList<T>> GetAll();
        Task<T> Insert(T item);
        Task Delete(T item);
        Task<T> GetById(int id);

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

        public async Task<T> Insert(T item)
        {
            await _dbManager.Database.InsertAsync(item);
            return item;
        }

        public async Task Delete(T item)
        {
            await _dbManager.Database.DeleteAsync(item);
        }

        public async Task<T> GetById(int id)
        {
            return await _dbManager.Database.Table<T>().FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}
