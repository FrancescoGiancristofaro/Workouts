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
using Services.Services;
using Syncfusion.Maui.Scheduler;
using WorkoutsApp.Extensions;
using WorkoutsApp.Pages.Templates.CustomViews;
using Font = Microsoft.Maui.Font;

namespace WorkoutsApp.Pages.Schedules
{
    public partial class SchedulesViewModel : BaseViewModel
    {
        private readonly IWorkoutService _workoutService;

        public IAsyncRelayCommand<SchedulerLongPressedEventArgs> LongPressedCommand { get; set; }
        public IAsyncRelayCommand<SchedulerTappedEventArgs> TappedCommand { get; set; }

        [ObservableProperty] ObservableCollection<SchedulerAppointment> _appointments = new();
        [ObservableProperty] View _relativeView;
        [ObservableProperty] bool _displayPopup;
        public SchedulesViewModel(IWorkoutService workoutService, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _workoutService = workoutService;
            LongPressedCommand = new AsyncRelayCommand<SchedulerLongPressedEventArgs>(InnerLongPressed);
            TappedCommand = new AsyncRelayCommand<SchedulerTappedEventArgs>(InnerTapped);
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
            var sessions = await _workoutService.GetWorkoutSessionsAsync();

            var woDic = (await _workoutService.GetWorkoutsAsync()).ToDictionary(x => x.Id);

            var opt = new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };
            var list = new ConcurrentBag<SchedulerAppointment>();
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
            Appointments = new ObservableCollection<SchedulerAppointment>(list);
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
            if (e.Element is not SchedulerElement.Appointment and not SchedulerElement.Header)
            {
                string action = await Shell.Current.DisplayActionSheet("ActionSheet: SavePhoto?", "Cancel", "Delete", "Photo Roll", "Email");
                return;
            }
        }
    }

}
