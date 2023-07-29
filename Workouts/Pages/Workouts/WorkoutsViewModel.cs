using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
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
        [ObservableProperty] bool _isRefreshing;


        public WorkoutsViewModel(IWorkoutService workoutService,ICacheService cacheService)
        {
            _workoutService = workoutService;
            _cacheService = cacheService;
            WeakReferenceMessenger.Default.Register<ValueChangedMessage<WorkoutOperation>>(this, async (r, m) =>
            {
                await RefreshWorkoutList();
            });
        }


        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task RefreshWorkoutList()
        {
            IsBusy = true;
            await RefreshWorkoutsList();
            IsRefreshing = false;
            IsBusy = false;
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task AddNewWorkout()
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
