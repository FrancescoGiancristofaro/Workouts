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

    }
    public class SeriesRepository : BaseRepository<Series> , ISeriesRepository
    {
        
    }
}
