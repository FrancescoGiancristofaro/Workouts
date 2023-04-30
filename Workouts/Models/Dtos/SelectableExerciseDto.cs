using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using WorkoutsApp.Extensions;
using WorkoutsApp.Models.DB;
using WorkoutsApp.Pages.Workouts;

namespace WorkoutsApp.Models.Dtos
{
    public partial class SelectableExerciseDto : ObservableObject
    {
        [ObservableProperty] Exercise _exercise;
        [ObservableProperty] bool _isSelected;
        [ObservableProperty] bool _isBeingDragged;
        [ObservableProperty] bool _isBeingDraggedOver;
        [ObservableProperty] ObservableCollection<SeriesDto> _series = new ();

    }
}
