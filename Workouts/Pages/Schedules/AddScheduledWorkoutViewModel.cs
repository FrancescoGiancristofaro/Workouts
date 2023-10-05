using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repositories.Models.Constants;
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
        [ObservableProperty] int _dayOfWeekIndex = -1;
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
            try
            {
                IsBusy = true;
                if (!IsConfigValid())
                {
                    await DisplayAlert("Attenzione", "Configurazione non valida");
                    return;
                }

                var schedule = new WorkoutsScheduledDto()
                {
                    IdWorkout = SelectedWorkout.Id.Value,
                    WorkoutName = SelectedWorkout.Name,
                    RecurrenceType = DailyRecurrence ? RecurrenceType.Daily : RecurrenceType.Weekly,
                    DayWeekRecurrence = (DayWeekRecurrence)DayOfWeekIndex,
                    RecurrenceEndType = RecurrenceEndByDate ? RecurrenceEndType.ByEndDate : RecurrenceEndAfterOccurences ? RecurrenceEndType.AfterTotOccurence : RecurrenceEndType.NoEnd,
                    Interval = DailyRecurrence ? (byte)DaysInterval : (byte)WeeksInterval,
                    StartDate = StartDate,
                    EndDate = EndDate,
                    NumOccurencesToEnd = (byte)OccurencesToEnd
                };

                await _workoutService.CreateWorkoutScheduleAsync(schedule);
                await Shell.Current.GoToAsync("..");
            }catch (Exception ex)
            {
                await ManageException(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public override async void PrepareModel()
        {
            base.PrepareModel();
            var workouts = await _workoutService.GetWorkoutsAsync();
            Workouts = new ObservableCollection<WorkoutsDto>(workouts);
        }

        private bool IsConfigValid()
        {
            var isValid = SelectedWorkout is not null;

            isValid = isValid && (DailyRecurrence ? DaysInterval > 0 : WeeksInterval > 0 && DayOfWeekIndex != -1);

            isValid = isValid && (RecurrenceEndByDate ? EndDate is not null 
                : RecurrenceEndAfterOccurences ? OccurencesToEnd > 0
                : isValid);

            return isValid;
        }
    }
}
