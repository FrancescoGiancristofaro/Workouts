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
        Task<IEnumerable<WorkoutExerciseDetails>> GetByWorkoutId(int id);
    }
    public class WorkoutExerciseDetailsRepository : BaseRepository<WorkoutExerciseDetails>, IWorkoutExerciseDetailsRepository
    {
        public async Task<IEnumerable<WorkoutExerciseDetails>> GetByWorkoutId(int id)
        {
            return (await GetAll()).Where(x => x.IdWorkout.Equals(id));
        }
    }
}
