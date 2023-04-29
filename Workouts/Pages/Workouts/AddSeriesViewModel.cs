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
        [ObservableProperty] private string _recoveryTime = "0";
        [ObservableProperty] private double _weight;

        public AddSeriesViewModel(IPopupService popupService) : base(popupService)
        {
        }
        [RelayCommand]
        async void AddSeries()
        {
            FormatRecoveryTime();

            StringBuilder sb = new StringBuilder(RecoveryTime);
            if (RecoveryTime[RecoveryTime.Length - 3] == ':')
                sb.Remove(RecoveryTime.Length-3, 1);
            string modifiedString = sb.ToString();
            var a = Int32.TryParse(modifiedString, out int recovedry);
            if (Reps <= 0 || Weight <= 0 || !Int32.TryParse(modifiedString, out int recovery))
            {
                await Shell.Current.DisplayAlert("Attenzione",
                    "Le ripetizioni,peso e tempo di recupero devono essere numeri maggiori di zero", "Ok");
                ;
                return;
            }
            _popupService.DismissPopup(new SeriesDto(){Repetitions = Reps,Weight = Weight,RecoveryTime = recovery });
        }

        [RelayCommand]
        void Format()
        {
            FormatRecoveryTime();

        }

        [RelayCommand]
        void Unformat()
        {
            if (RecoveryTime.Length > 2 && RecoveryTime[RecoveryTime.Length - 3] == ':')
            {
                var list = RecoveryTime.ToList();
                list.RemoveAt(RecoveryTime.Length - 3);
                if(Int32.TryParse(new string(list.ToArray()),out int d))
                {
                    RecoveryTime = d.ToString();
                    return;
                }
                RecoveryTime = "0";
                return;
            }
        }

        public override void Opened()
        {
            var data = _popupService.GetPopupData();
            if (data is SeriesDto dto)
            {
                Reps = dto.Repetitions;
                Weight = dto.Weight;
                RecoveryTime = dto.RecoveryTime.ToString();
                FormatRecoveryTime();
            }

        }

        private void FormatRecoveryTime()
        {
            if (RecoveryTime.Length > 2)
            {
                if (RecoveryTime[RecoveryTime.Length - 3] == ':')
                {
                    return;
                }
                var list = RecoveryTime.ToList();
                list.Insert(RecoveryTime.Length - 2, ':');
                RecoveryTime = new string(list.ToArray());
                return;
            }
            else if (RecoveryTime.Length == 2)
            {
                var list2 = RecoveryTime.ToList();
                list2.Insert(0, ':');
                list2.Insert(0, '0');
                RecoveryTime = new string(list2.ToArray());
            }
            else if (RecoveryTime.Length == 1)
            {
                var list2 = RecoveryTime.ToList();
                list2.Insert(0, '0');
                list2.Insert(0, ':');
                list2.Insert(0, '0');
                RecoveryTime = new string(list2.ToArray());
            }
        }

    }
}
