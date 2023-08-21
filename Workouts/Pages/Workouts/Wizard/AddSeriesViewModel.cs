using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services.Dtos;
using WorkoutsApp.Extensions;
using WorkoutsApp.Dtos;
using WorkoutsApp.Services;

namespace WorkoutsApp.Pages.Workouts
{
    public partial class AddSeriesViewModel : BasePopupViewModel
    {
        [ObservableProperty] private string _reps;
        [ObservableProperty] private string _recoveryTime;
        [ObservableProperty] private string _weight;

        private bool _notExecuteTextChanged = false;
        public AddSeriesViewModel(IPopupService popupService) : base(popupService)
        {
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        async Task AddSeries()
        {
            Unfocused();

            var splittedRecovery = string.IsNullOrEmpty(RecoveryTime) ? new string[2] : RecoveryTime.Split(':');

            if (!Int32.TryParse(Reps, out int reps) ||
                !Int32.TryParse(Weight, out int weight) ||
                !Int32.TryParse(splittedRecovery[0], out int minutes) ||
                !Int32.TryParse(splittedRecovery[1], out int seconds) ||
                reps <=0 || weight <=0 || (minutes <=0 && seconds <=0))
            {
                await Shell.Current.DisplayAlert("Attenzione",
                    "Le ripetizioni,peso e tempo di recupero devono essere numeri maggiori di zero", "Ok");
                ;
                return;
            }
            _popupService.DismissPopup(new SeriesDto(){Repetitions = reps,Weight = weight,SecondsRecoveryTime = (minutes * 60)+seconds });
        }


        [RelayCommand]
        void TextChanged(object eventArgs)
        {
            if (_notExecuteTextChanged)
            {
                _notExecuteTextChanged = false;
                return;
            }

            var args = eventArgs as TextChangedEventArgs;
            if (args.NewTextValue.SafeAny(x => x is not ':' && !Char.IsDigit(x)))
            {
                RecoveryTime = "";
                _notExecuteTextChanged = true;
                return;
            }

            if (args.NewTextValue?.Length > args.OldTextValue?.Length)
            {
                if (args.NewTextValue?.Length == 3 && !args.NewTextValue.SafeAny(x => x is ':'))
                {
                    RecoveryTime = args.NewTextValue.Insert(1, ":");
                    _notExecuteTextChanged = true;
                    return;
                }

                if (args.NewTextValue?.Length == 5)
                {
                    var index = args.NewTextValue.IndexOf(':');
                    var a = args.NewTextValue.Remove(index, 1);
                    a = a.Insert(2, ":");
                    RecoveryTime = a;
                    _notExecuteTextChanged = true;
                    return;
                }

                if (args.NewTextValue?.Length < 3)
                {
                    var index = args.NewTextValue.IndexOf(':');
                    if(index is -1)
                        return;

                    args.NewTextValue.Remove(index);
                    RecoveryTime = args.NewTextValue;
                    _notExecuteTextChanged = true;
                    return;
                }
            }
            else
            {
                if (args.NewTextValue?.Length == 4)
                {
                    var index = args.NewTextValue.IndexOf(':');
                    var a = args.NewTextValue.Remove(index, 1);
                    a = a.Insert(1, ":");
                    RecoveryTime = a;
                    _notExecuteTextChanged = true;
                    return;
                }

                if (args.NewTextValue?.Length == 3)
                {
                    var index = args.NewTextValue.IndexOf(':');
                    RecoveryTime = args.NewTextValue.Remove(index, 1);
                    _notExecuteTextChanged = true;
                    return;
                }
            }
            
        }

        [RelayCommand]

        void Unfocused()
        {
            if (string.IsNullOrEmpty(RecoveryTime) || RecoveryTime.Contains(':'))
                return;

            _notExecuteTextChanged = true;
            var secondsChoiced = Int32.Parse(RecoveryTime.Replace(":", ""));
            int minutes = secondsChoiced / 60;
            minutes = minutes > 99 ? 99 : minutes;
            int seconds = secondsChoiced % 60;
            string secondsString = seconds < 10 ? "0" + seconds : $"{seconds}";
            RecoveryTime = $"{minutes}:{secondsString}";
        }
        public override void Opened()
        {
            _notExecuteTextChanged = true;
            var data = _popupService.GetPopupData();
            if (data is SeriesDto dto)
            {
                Reps = dto.Repetitions.ToString();
                Weight = dto.Weight.ToString();
                RecoveryTime = dto.SecondsRecoveryTime.ToString();
                Unfocused();
            }

        }
        

    }
}
