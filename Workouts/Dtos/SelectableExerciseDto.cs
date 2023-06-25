using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Repositories.Models;
using Services.Dtos;
using WorkoutsApp.Models.Dtos;

namespace WorkoutsApp.Dtos
{
    public partial class SelectableExerciseDto : ObservableObject
    {
        [ObservableProperty] ExerciseDto _exercise;
        [ObservableProperty] bool _isSelected;
    }
}
