using Repositories.Models;
using Repositories.Settings;
using SQLite;

namespace Repositories.Repositories
{
    public interface IRepository<T> where T : IAmAModel, new()
    {
        Task<IList<T>> GetAll();
        Task<T> Insert(T item);
        Task Update(T item);
        Task Delete(T item);
        Task<T> GetById(int id);
        Task TryDeleteAllAsync();
        Task DeleteAllAsync();
        Task Init();
        Task DeleteTable();
        Task RecreateTableWithData();

        Task<TableMapping> GetMapping();

    }
    public class BaseRepository<T> : IRepository<T> where T : IAmAModel, new()
    {
        protected readonly SQLiteAsyncConnection Database;

        protected BaseRepository()
        {
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var path = Path.Combine(basePath, SqlLiteSettings.DatabaseFilename);
            Database = new SQLiteAsyncConnection(path, SqlLiteSettings.Flags);
        }

        public async Task<IList<T>> GetAll()
        {
            return await Database.Table<T>().ToListAsync();
        }

        public async Task<T> Insert(T item)
        {
            await Database.InsertAsync(item);
            return item;
        }

        public async Task Delete(T item)
        {
            await Database.DeleteAsync(item);
        }

        public async Task<T> GetById(int id)
        {
            return await Database.Table<T>().FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task TryDeleteAllAsync()
        {
            try
            {
                await DeleteAllAsync();
            }
            catch (SQLiteException e)
            {
                //it's ok, table doesn't exist
            }
        }

        public Task DeleteAllAsync()
        {
            return Database.DeleteAllAsync<T>();
        }
        public Task Init()
        {
            return Database.CreateTableAsync<T>();
        }
        public Task DeleteTable()
        {
            return Database.DropTableAsync<T>();
        }

        public async Task RecreateTableWithData()
        {
            var allItems = await GetAll();
            await DeleteTable();
            await Init();
            foreach (var item in allItems)
            {
                await Insert(item);
            }
        }

        public Task<TableMapping> GetMapping()
        {
            return Database.GetMappingAsync(typeof(T));
        }

        public async Task Update(T item)
        {
            await Database.UpdateAsync(item);
        }
    }
}
