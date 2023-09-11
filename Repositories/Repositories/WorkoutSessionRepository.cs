using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Models;

namespace Repositories.Repositories
{

    public interface IWorkoutSessionRepository : IRepository<WorkoutSession>
    {

    }
    public class WorkoutSessionRepository : BaseRepository<WorkoutSession>, IWorkoutSessionRepository
    {

    }
}
