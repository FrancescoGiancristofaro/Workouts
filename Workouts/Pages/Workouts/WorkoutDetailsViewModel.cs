using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services.Constants;
using Services.Dtos;
using Services.Services;
using WorkoutsApp.Dtos;

namespace WorkoutsApp.Pages.Workouts
{
    [QueryProperty(nameof(WorkoutsDto), "workout")]
    public partial class WorkoutDetailsViewModel : BaseViewModel
    {
        private readonly IExerciseService _exerciseService;
        private readonly ICacheService _cacheService;
        [ObservableProperty] WorkoutsDto _workoutsDto;

        [RelayCommand(AllowConcurrentExecutions = false)]
        async Task StartWorkout()
        {
            var cachedWorkoutSession = WorkoutSessionWizardDto.Create(WorkoutsDto.Id.Value);

            _cacheService.Add(CacheKeys.WorkoutSessionProgression, cachedWorkoutSession);

            var exDetails = (await _exerciseService.GetAllExerciseDetailsByWorkoutIdAsync(WorkoutsDto.Id.Value))
                .Select(x => new SelectableWorkoutsExerciseDetailsDto() { IsSelected = false, ExerciseDetail = x })
                .ToList();
           await GoToAsync(AppRoutes.ExerciseSessionPage, "exsessionlist", exDetails);
        }

        public WorkoutDetailsViewModel(IExerciseService exerciseService, ICacheService cacheService, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _exerciseService = exerciseService;
            _cacheService = cacheService;
        }
    }
}
