using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dtos
{
    public class WorkoutExerciseDetailsDto
    {
        public int? Id { get; set; }
        public int IdWorkout { get; set; }
        public int IdExercise { get; set; }
        public int OrderInWorkout { get; set; }
        public string Note { get; set; }
    }
}
