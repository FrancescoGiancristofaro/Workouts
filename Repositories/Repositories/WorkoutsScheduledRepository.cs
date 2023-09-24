using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Models;

namespace Repositories.Repositories
{
    public interface IWorkoutsScheduledRepository : IRepository<WorkoutsScheduled>
    {

    }
    public class WorkoutsScheduledRepository : BaseRepository<WorkoutsScheduled>, IWorkoutsScheduledRepository
    {

    }
}
