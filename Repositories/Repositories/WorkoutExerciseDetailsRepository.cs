using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Models;
using Repositories.Settings;

namespace Repositories.Repositories
{
    public interface IWorkoutExerciseDetailsRepository : IRepository<WorkoutExerciseDetails>
    {

    }
    public class WorkoutExerciseDetailsRepository : BaseRepository<WorkoutExerciseDetails>, IWorkoutExerciseDetailsRepository
    {
        
    }
}
