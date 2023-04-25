using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkoutsApp.Models.Dtos;
using WorkoutsApp.Services;

namespace WorkoutsApp.Pages.Workouts
{
    public partial class AddSeriesViewModel : BasePopupViewModel
    {
        [ObservableProperty] private int _reps;
        [ObservableProperty] private int _recoveryTime;
        [ObservableProperty] private double _weight;

        public AddSeriesViewModel(IPopupService popupService) : base(popupService)
        {
        }
        [RelayCommand]
        async void AddSeries()
        {
            if (Reps == 0 || Weight == 0)
            {
                await Shell.Current.DisplayAlert("Attenzione",
                    "Le ripetizioni e il peso non possono essere pari a zero", "Ok");
                return;
            }
            _popupService.DismissPopup(new SeriesDto(){Repetitions = Reps,Weight = Weight,RecoveryTime = RecoveryTime});
        }

        public override void Opened()
        {
            var data = _popupService.GetPopupData();
            if (data is SeriesDto dto)
            {
                Reps = dto.Repetitions;
                Weight = dto.Weight;
                RecoveryTime = dto.RecoveryTime;
            }

        }
    }
}
