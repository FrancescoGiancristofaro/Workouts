using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Services.Services;
using Syncfusion.Maui.Scheduler;
using WorkoutsApp.Extensions;
using WorkoutsApp.Helpers;
using WorkoutsApp.Pages.Templates.CustomViews;
using Font = Microsoft.Maui.Font;

namespace WorkoutsApp.Pages.Schedules
{
    public partial class SchedulesViewModel : BaseViewModel
    {
        private readonly IWorkoutService _workoutService;

        public IAsyncRelayCommand<SchedulerLongPressedEventArgs> LongPressedCommand { get; set; }
        public IAsyncRelayCommand<SchedulerTappedEventArgs> TappedCommand { get; set; }

        [ObservableProperty] ObservableCollection<WorkoutAppointment> _appointments = new();
        [ObservableProperty] View _relativeView;
        [ObservableProperty] bool _displayPopup;
        public SchedulesViewModel(IWorkoutService workoutService, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _workoutService = workoutService;
            LongPressedCommand = new AsyncRelayCommand<SchedulerLongPressedEventArgs>(InnerLongPressed);
            TappedCommand = new AsyncRelayCommand<SchedulerTappedEventArgs>(InnerTapped);

            WeakReferenceMessenger.Default.Register<ValueChangedMessage<WorkoutOperation>>(this, async (r, m) =>
            {
                await RefreshSchedulesList();
            });
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        async Task AddScheduledWorkout()
        {
            await Shell.Current.GoToAsync(AppRoutes.AddScheduledWorkoutPage);
        }
        public override async void OnAppearing()
        {
            base.PrepareModel();
            await MauiProgram.InitialStartupTask;
            await RefreshSchedulesList();
        }
        private async Task InnerTapped(SchedulerTappedEventArgs e)
        {
            if (e.Element is SchedulerElement.Appointment)
            {
                if (e.Appointments.First() is WorkoutDoneAppointment appointment)
                {
                    ShowPopup(typeof(WorkoutSessionDetailPopup), appointment.WorkoutTypeId);
                }

                return;
            }
        }
        private async Task InnerLongPressed(SchedulerLongPressedEventArgs e)
        {
            if (e.Element is SchedulerElement.Appointment && e.Appointments.First() is WorkoutToDoAppointment appointment)
            {
                var res = await Shell.Current.DisplayAlert("Attenzione", "Vuoi cancellare la ricorrenza? L'operazione è irreversibile", "Ok", "Annulla");
                if (res)
                    await _workoutService.RemoveWorkoutSchedule(appointment.WorkoutTypeId);
                return;
            }
        }

        private async Task RefreshSchedulesList()
        {
            try
            {
                IsBusy = true;

                var sessions = await _workoutService.GetWorkoutSessionsAsync();

                var woDic = (await _workoutService.GetWorkoutsAsync()).ToDictionary(x => x.Id);

                var opt = new ParallelOptions
                {
                    MaxDegreeOfParallelism = Environment.ProcessorCount
                };

                var list = new ConcurrentBag<WorkoutAppointment>();
                Parallel.ForEach(sessions, (item, cancellationToken) =>
                {
                    var wo = woDic[item.IdWorkout];
                    list.Add(new WorkoutDoneAppointment
                    {
                        Background = Colors.LightGreen,
                        StartTime = item.StartDate,
                        EndTime = item.EndDate,
                        Subject = wo.Name,
                        WorkoutTypeId = item.Id
                    });
                });

                var schedules = await _workoutService.GetWorkoutsScheduledAsync();
                Parallel.ForEach(schedules, (item, cancellationToken) =>
                {
                    //if (item.StartDate - DateTime.UtcNow < TimeSpan.Zero)
                    //{
                    //    var pastRecurrence =(new WorkoutNotDoneAppointment
                    //    {
                    //        Background = Colors.Red,
                    //        StartTime = item.StartDate,
                    //        EndTime = DateTime.UtcNow.AddDays(-1),
                    //        Subject = item.WorkoutName,
                    //        RecurrenceRule = SyncfusionSchedulerHelper.GetRecurrenceRule(item, true),
                    //        WorkoutTypeId = item.Id
                    //    });
                    //    var alreadyDone = sessions.Where(x => (x.StartDate - item.StartDate) > TimeSpan.Zero);
                    //    if (alreadyDone.Any())
                    //    {
                    //        pastRecurrence.RecurrenceExceptionDates = new ObservableCollection<DateTime>();
                    //        foreach (var session in alreadyDone)
                    //            pastRecurrence.RecurrenceExceptionDates.Add(session.StartDate);
                    //    }
                    //    list.Add(pastRecurrence);
                    //}


                    var recurrence = new WorkoutToDoAppointment
                    {
                        Background = Colors.LightGray,
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now.AddHours(1),
                        Subject = item.WorkoutName,
                        RecurrenceRule = SyncfusionSchedulerHelper.GetRecurrenceRule(item),
                        WorkoutTypeId = item.Id
                    };
                    if (sessions.Any(x=>x.StartDate.IsSameDayThan(DateTime.Now)))
                        recurrence.RecurrenceExceptionDates = new ObservableCollection<DateTime>()
                        {
                            DateTime.Now
                        };
                    list.Add(recurrence);
                });

                Appointments = new ObservableCollection<WorkoutAppointment>(list);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }

}
