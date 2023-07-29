using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Repositories.Models
{
    public class Series : IAmAModel
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
        public int IdWorkout { get; set; }
        public int IdExercise { get; set; }
        public int Repetitions { get; set; }
        public int RecoveryTime { get; set; }
        public double Weight { get; set; }
    }
}
