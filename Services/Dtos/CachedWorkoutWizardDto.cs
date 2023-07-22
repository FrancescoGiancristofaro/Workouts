using Repositories.Models;
using Services.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dtos
{
    public class WorkoutWizardDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ExerciseDto> ExerciseList { get;  set; }
        public List<SeriesDto>[] SeriesList { get; set; }
        
        public static WorkoutWizardDto Create(string name, string description,IEnumerable<ExerciseDto> exercises)
        {
            return new WorkoutWizardDto()
            {
                Name = name,
                Description = description,
                ExerciseList = new (exercises),
                SeriesList = new List<SeriesDto>[exercises.Count()]
            };
        }

        public void InsertSeries(ExerciseDto exercise , SeriesDto series)
        {
            var ex = ExerciseList.FirstOrDefault(x => x.Id == exercise.Id.Value);
            if (ex is null)
                return;
            var index = ExerciseList.IndexOf(ex);
            SeriesList[index] ??= new List<SeriesDto>();
            SeriesList[index].Add(series);
        }

        public void DeleteSeries(ExerciseDto exercise, SeriesDto series)
        {
            var ex = ExerciseList.FirstOrDefault(x => x.Id == exercise.Id.Value);
            if (ex is null)
                return;
            var index = ExerciseList.IndexOf(ex);
            SeriesList[index].Remove(series);
        }
    }
}
