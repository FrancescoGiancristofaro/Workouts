using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services.Dtos;
using Services.Services;

namespace WorkoutsApp.Pages.Workouts
{
    [QueryProperty(nameof(WorkoutsDto), "workout")]
    public partial class WorkoutDetailsViewModel : BaseViewModel
    {
        private readonly IExerciseService _exerciseService;
        [ObservableProperty] WorkoutsDto _workoutsDto;

        [RelayCommand(AllowConcurrentExecutions = false)]
        async Task StartWorkout()
        {

        }

        public WorkoutDetailsViewModel(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }
    }
}
