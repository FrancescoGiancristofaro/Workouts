using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services.Dtos;
using Services.Services;

namespace WorkoutsApp.Pages.Schedules
{
    public partial class AddScheduledWorkoutViewModel : BaseViewModel
    {

        private readonly IWorkoutService _workoutService;

        [ObservableProperty] ObservableCollection<WorkoutsDto> _workouts;
        [ObservableProperty] WorkoutsDto _selectedWorkout;
        [ObservableProperty] bool _dailyRecurrence = true;
        [ObservableProperty] int _daysInterval;
        [ObservableProperty] int _weeksInterval;
        [ObservableProperty] int _dayOfWeekIndex;
        [ObservableProperty] DateTime _startDate = DateTime.Now;
        [ObservableProperty] bool _recurrenceEndByDate = false;
        [ObservableProperty] bool _recurrenceEndAfterOccurences = false;
        [ObservableProperty] bool _recurrenceNoEnd = true;
        [ObservableProperty] DateTime? _endDate = DateTime.Now;
        [ObservableProperty] int _occurencesToEnd;


        public AddScheduledWorkoutViewModel(
            IServiceProvider serviceProvider,
            IWorkoutService workoutService) : base(serviceProvider)
        {
            _workoutService = workoutService;
        }

        [RelayCommand(AllowConcurrentExecutions =false)]
        async Task Save()
        {

        }

        public override async void PrepareModel()
        {
            base.PrepareModel();
            var workouts = await _workoutService.GetWorkoutsAsync();
            Workouts = new ObservableCollection<WorkoutsDto>(workouts);
        }
    }
}
