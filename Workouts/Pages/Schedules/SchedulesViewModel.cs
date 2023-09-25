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
using WorkoutsApp.Services;
using Font = Microsoft.Maui.Font;

namespace WorkoutsApp.Pages.Schedules
{
    public partial class SchedulesViewModel : BaseViewModel
    {
        private readonly IWorkoutService _workoutService;
        private readonly IPopupService _popupService;

        public IAsyncRelayCommand<SchedulerLongPressedEventArgs> LongPressedCommand { get; set; }

        [ObservableProperty] ObservableCollection<SchedulerAppointment> _appointments = new ();
        [ObservableProperty] View _relativeView;
        [ObservableProperty] bool _displayPopup;
        public SchedulesViewModel(
            IWorkoutService workoutService,
            IPopupService popupService)
        {
            _workoutService = workoutService;
            _popupService = popupService;
            LongPressedCommand = new AsyncRelayCommand<SchedulerLongPressedEventArgs>(InnerLongPressed);
        }
       

        public override async void OnAppearing()
        {
            base.PrepareModel();
            await MauiProgram.InitialStartupTask;
            var sessions = await _workoutService.GetWorkoutSessionsAsync();

            var woDic = (await _workoutService.GetWorkoutsAsync()).ToDictionary(x=>x.Id);

            var opt = new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };
            var list = new ConcurrentBag<SchedulerAppointment>();
            Parallel.ForEach(sessions, (item, cancellationToken) =>
            {
                var wo = woDic[item.IdWorkout];
                list.Add(new SchedulerAppointment
                {
                    StartTime = item.StartDate,
                    EndTime = item.EndDate,
                    Subject = wo.Name
                });
            });
            Appointments = new ObservableCollection<SchedulerAppointment>(list);
        }

        private async Task InnerLongPressed(SchedulerLongPressedEventArgs e)
        {
            //CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            //var snackbarOptions = new SnackbarOptions
            //{
            //    BackgroundColor = Colors.Red,
            //    TextColor = Colors.Green,
            //    ActionButtonTextColor = Colors.Yellow,
            //    CornerRadius = new CornerRadius(10),
            //    Font = Font.SystemFontOfSize(14),
            //    ActionButtonFont = Font.SystemFontOfSize(14),
            //    CharacterSpacing = 0.5
            //};

            //string text = "This is a Snackbar";
            //string actionButtonText = "Click Here to Dismiss";
            //Action action = async () => await Shell.Current.CurrentPage.DisplayAlert("Snackbar ActionButton Tapped", "The user has tapped the Snackbar ActionButton", "OK");
            //TimeSpan duration = TimeSpan.FromSeconds(3);

            //var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);

            //await snackbar.Show();
            //await Shell.Current.CurrentPage.DisplaySnackbar("s", "s", "s", new[] { "a", "b" });
            //_popupService.
        }
    }

}
