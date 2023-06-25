using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Repositories.Models;
using WorkoutsApp.Models.Dtos;

namespace WorkoutsApp.Dtos
{
    public partial class SelectableExerciseDto : ObservableObject
    {
        [ObservableProperty] Exercise _exercise;
        [ObservableProperty] bool _isSelected;
    }
}
