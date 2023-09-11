using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dtos
{
    public class WorkoutSessionWizardDto
    {
        public int IdWorkout { get; set; }

        public List<ExerciseSeriesDto> SessionExercises { get; set; } = new ();
        public static WorkoutSessionWizardDto Create(int workoutId)
        {
            return new WorkoutSessionWizardDto { IdWorkout = workoutId };
        }

        public void InsertExercise(WorkoutExerciseDetailsDto exDto, IEnumerable<SeriesDto> series)
        {
            var ex = SessionExercises.FirstOrDefault(x => x.IdWorkoutExercise == exDto.Id);
            if (ex is null)
            {
                SessionExercises.Add(new ExerciseSeriesDto { IdWorkoutExercise = exDto.Id, Note = exDto.Note, Series = new List<SeriesDto>(series) });
                return;
            }
            ex = new ExerciseSeriesDto { IdWorkoutExercise = exDto.Id, Series = new List<SeriesDto>(series) };
        }
    }

    public class WorkoutSessionDto
    {
        public int Id { get; set; }
        public int IdWorkout { get; set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
    }
}
