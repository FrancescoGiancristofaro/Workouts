using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services.Dtos;
using Services.Services;
using WorkoutsApp.Dtos;

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
            var exDetails = (await _exerciseService.GetAllExerciseDetailsByWorkoutIdAsync(WorkoutsDto.Id.Value))
                .Select(x => new SelectableWorkoutsExerciseDetailsDto() { IsSelected = false, ExerciseDetail = x })
                .ToList();
           await GoToAsync(AppRoutes.ExerciseSessionPage, "exsessionlist", exDetails);
        }

        public WorkoutDetailsViewModel(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }
    }
}
