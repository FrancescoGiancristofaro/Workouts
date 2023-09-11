using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Models;

namespace Repositories.Repositories
{
    public interface ISeriesHistoryRepository : IRepository<SeriesHistory>
    {
        Task<List<SeriesHistory>> GetSeriesHistoryByExerciseDetailId(int exId);

    }
    public class SeriesHistoryRepository : BaseRepository<SeriesHistory>, ISeriesHistoryRepository
    {
        public Task<List<SeriesHistory>> GetSeriesHistoryByExerciseDetailId(int exId)
        {
            return  Database.Table<SeriesHistory>()
                .Where(x => x.IdWorkoutExerciseDetails.Equals(exId)).ToListAsync();
        }
    }
}
