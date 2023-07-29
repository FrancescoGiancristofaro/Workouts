using Repositories.Models;
using System;
using System.Collections.Generic;
namespace Repositories.Repositories
{
    public interface IMasterRepository : IRepository<Master>
    {
        Task<List<Column>> GetTableInfo(string tableName);
    }

    public class MasterRepository : BaseRepository<Master>, IMasterRepository
    {
        public async Task<List<Column>> GetTableInfo(string tableName)
        {
            var result = await Database.QueryAsync<Column>($"pragma table_info({tableName})");
            return result;
        }

        public new Task Init()
        {
            throw new Exception("Dont use this method");
        }
        public new Task DeleteAsync(Master model)
        {
            throw new Exception("Dont use this method");
        }
        public new Task DeleteTable()
        {
            throw new Exception("Dont use this method");
        }
    }
}
