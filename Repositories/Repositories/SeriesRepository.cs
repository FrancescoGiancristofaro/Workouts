using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Models;
using Repositories.Settings;

namespace Repositories.Repositories
{
    public interface ISeriesRepository : IRepository<Series>
    {
        Task<List<Series>> GetSeriesByExerciseDetailId(int exId);
    }
    public class SeriesRepository : BaseRepository<Series> , ISeriesRepository
    {
        public Task<List<Series>> GetSeriesByExerciseDetailId(int exId)
        {
            return Database.Table<Series>()
                .Where(x => x.IdWorkoutExerciseDetails.Equals(exId)).ToListAsync();
        }
    }
}
