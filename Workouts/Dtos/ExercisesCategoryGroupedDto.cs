using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Repositories.Constants;

namespace WorkoutsApp.Dtos
{
    public class ExercisesCategoryGroupedDto : List<SelectableExerciseDto>
    {
        public ExerciseCategory Category { get; set; }
        public ExercisesCategoryGroupedDto(ExerciseCategory category, List<SelectableExerciseDto> exerciseDtos) : base(exerciseDtos)
        {
            Category = category;
        }
    }
    
}
