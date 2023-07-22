using CommunityToolkit.Mvvm.ComponentModel;
using Services.Dtos;

namespace WorkoutsApp.Dtos
{
    public partial class SelectableExerciseDto : ObservableObject
    {
        [ObservableProperty] ExerciseDto _exercise;
        [ObservableProperty] bool _isSelected;
    }
}
