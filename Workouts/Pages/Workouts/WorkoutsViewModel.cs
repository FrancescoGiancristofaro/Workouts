using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services.Services;
using WorkoutsApp.Extensions;
using Repositories.Models;
using Services.Constants;
using Services.Dtos;
using WorkoutsApp.Services;


namespace WorkoutsApp.Pages.Workouts
{
    public partial class WorkoutsViewModel : BaseViewModel
    {
        private readonly IWorkoutService _workoutService;
        private readonly ICacheService _cacheService;

        [ObservableProperty] ObservableCollection<WorkoutsDto> _workouts;

        [ObservableProperty] bool _isWorkoutsListEmpty;


        public WorkoutsViewModel(IWorkoutService workoutService,ICacheService cacheService)
        {
            _workoutService = workoutService;
            _cacheService = cacheService;
        }


        [RelayCommand]
        public async void AddNewWorkout()
        {
            await Shell.Current.GoToAsync(AppRoutes.AddNewWorkoutPage);
        }
        public override async void OnAppearing()
        {
            IsBusy = true;
            try
            {
                var cachedWorkoutWizard = _cacheService.Get<WorkoutWizardDto>(CacheKeys.WorkoutWizardProgression);
                if (cachedWorkoutWizard is not null)
                {
                    var res = await Shell.Current.DisplayAlert("Attenzione",
                        "C'è una creazione di allenamento in sospeso vuoi prosieguirla?", "Ok", "Annulla");
                    _cacheService.Remove(CacheKeys.WorkoutWizardProgression);
                }
                await RefreshWorkoutsList();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task RefreshWorkoutsList()
        {
            Workouts = new ObservableCollection<WorkoutsDto>();
            var wo = await _workoutService.GetWorkoutsAsync();
            foreach (var item in wo)
            {
                Workouts.Add(item);
            }
            IsWorkoutsListEmpty = !Workouts.SafeAny();
        }



    }
}
