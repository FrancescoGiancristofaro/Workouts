using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutsApp.Models.Dtos
{
    public partial class SeriesDto : ObservableObject
    {
        [ObservableProperty]  int _repetitions;
        [ObservableProperty]  int _secondsRecoveryTime;
        [ObservableProperty] double _weight;
    }
}
