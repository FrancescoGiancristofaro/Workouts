using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Services.Dtos
{
    public enum RecoveryStatus
    {
        Running,
        Paused,
        Stopped
    }

    public partial class ExecutableSeriesDto : ObservableObject
    {
        [ObservableProperty] SeriesDto _series;
        [ObservableProperty] int _secondsLeft;
        [ObservableProperty] RecoveryStatus _recoveryStatus;
        private Timer? _timer;
        public void StartTimer(bool resume = false)
        {
            RecoveryStatus = RecoveryStatus.Running;
            _timer = new Timer(1000); 
            _timer.Elapsed += TimerElapsed;
            if (!resume)
                SecondsLeft = Series.SecondsRecoveryTime;
            _timer.Start();
        }
        public void PauseTimer()
        {
            RecoveryStatus = RecoveryStatus.Paused;
            _timer?.Stop();
        }
        public void StopTimer()
        {
            RecoveryStatus = RecoveryStatus.Stopped;
            SecondsLeft = Series.SecondsRecoveryTime;
            _timer?.Stop();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (SecondsLeft > 0)
            {
                SecondsLeft--;
                return;
            }
            StopTimer();
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<RecoveryStatus>(RecoveryStatus.Stopped));
        }


    }

}
