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
                await this.DisplayAlert("ad", "vedere allenamento fatto + opzione inizia");
                return;
            }
        }
        private async Task InnerLongPressed(SchedulerLongPressedEventArgs e)
        {
            if (e.Element is SchedulerElement.Appointment && e.Appointments.First() is WorkoutToDoAppointment appointment)
            {
                var res = await Shell.Current.DisplayAlert("Attenzione", "Vuoi cancellare la ricorrenza? L'operazione è irreversibile", "Ok", "Annulla");
                if (res)
                    await _workoutService.RemoveWorkoutSchedule(appointment.WorkoutId);
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
                        Subject = wo.Name
                    });
                });

                var schedules = await _workoutService.GetWorkoutsScheduledAsync();
                Parallel.ForEach(schedules, (item, cancellationToken) =>
                {
                    //if(item.StartDate - DateTime.UtcNow < TimeSpan.Zero)
                    //{
                    //    list.Add(new WorkoutNotDoneAppointment
                    //    {
                    //        Background = Colors.Red,
                    //        StartTime = item.StartDate,
                    //        EndTime = item.StartDate.AddHours(1),
                    //        Subject = item.WorkoutName,
                    //        RecurrenceRule = SyncfusionSchedulerHelper.GetRecurrenceRule(item, true)
                    //    });
                    //}
                    var recurrence = new WorkoutToDoAppointment
                    {
                        Background = Colors.LightGray,
                        StartTime = DateTime.UtcNow,
                        EndTime = DateTime.UtcNow.AddHours(1),
                        Subject = item.WorkoutName,
                        RecurrenceRule = SyncfusionSchedulerHelper.GetRecurrenceRule(item),
                        WorkoutId = item.Id
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
