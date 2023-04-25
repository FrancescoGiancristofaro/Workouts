using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using WorkoutsApp.Models.DB;

namespace WorkoutsApp.Models.Dtos
{
    public partial class SelectableExerciseDto : ObservableObject
    {
        [ObservableProperty] Exercise _exercise;
        [ObservableProperty] bool _isSelected;
        [ObservableProperty] ObservableCollection<SeriesDto> _series = new ();

    }
}
