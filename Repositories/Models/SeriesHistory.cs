using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Repositories.Models
{
    public class SeriesHistory : IAmAModel
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
        public int IdWorkoutSession { get; set; }
        public int IdWorkoutExerciseDetails { get; set; }
        public int Repetitions { get; set; }
        public int RecoveryTime { get; set; }
        public double Weight { get; set; }
    }
}
