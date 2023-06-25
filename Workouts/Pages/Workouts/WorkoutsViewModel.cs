using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services.Services;
using WorkoutsApp.Extensions;
using Repositories.Models;
using Services.Dtos;
using WorkoutsApp.Services;


namespace WorkoutsApp.Pages.Workouts
{
    public partial class WorkoutsViewModel : BaseViewModel
    {
        private readonly IWorkoutService _workoutService;

        [ObservableProperty] ObservableCollection<WorkoutsDto> _workouts;

        [ObservableProperty] bool _isWorkoutsListEmpty;

        [RelayCommand]
        public async void AddNewWorkout()
        {
            await Shell.Current.GoToAsync(AppRoutes.AddNewWorkoutPage);
        }

        public WorkoutsViewModel(IWorkoutService workoutService) 
        {
            _workoutService = workoutService;
        }

        protected override async Task Appearing()
        {
            IsBusy = true;
            try
            {
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
