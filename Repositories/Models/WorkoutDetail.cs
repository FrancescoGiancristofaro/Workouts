using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Repositories.Models
{
    public class WorkoutDetail : IAmAModel
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
        public int IdWorkout { get; set; }
        public int IdExercise { get; set; }
    }
}
