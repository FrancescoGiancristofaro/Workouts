using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Repositories.Models
{
    /// <summary>
    /// Represent an exercise instance inside a workout 
    /// </summary>
    public class WorkoutExerciseDetails : IAmAModel
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
        public int IdWorkout { get; set; }
        public int IdExercise { get; set; }
        public int OrderInWorkout { get; set; }
        public string Note { get; set; }
    }
}
