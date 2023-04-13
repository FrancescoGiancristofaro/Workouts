using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using WorkoutsApp.Extensions;
using WorkoutsApp.Services;


namespace WorkoutsApp.Pages.Workouts
{
    public partial class WorkoutsViewModel : BaseViewModel
    {
        private readonly IWorkoutService _workoutService;

        [ObservableProperty]
        ObservableCollection<Models.DB.Workouts> workouts;

        [ObservableProperty]
        bool isWorkoutsListEmpty;

        public WorkoutsViewModel(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        protected override async void Appearing()
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
            Workouts = new ObservableCollection<Models.DB.Workouts>();
            var wo = await _workoutService.GetAllWorkouts();
            foreach (var item in wo)
            {
                Workouts.Add(item);
            }
            IsWorkoutsListEmpty = Workouts.SafeAny();
        }



    }
}
