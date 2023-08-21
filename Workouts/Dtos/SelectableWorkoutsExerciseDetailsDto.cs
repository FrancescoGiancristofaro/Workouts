using CommunityToolkit.Mvvm.ComponentModel;
using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutsApp.Dtos
{
    public partial class SelectableWorkoutsExerciseDetailsDto : ObservableObject
    {
        [ObservableProperty] WorkoutExerciseDetailsDto _exerciseDetail;
        [ObservableProperty] bool _isSelected;
    }
}
