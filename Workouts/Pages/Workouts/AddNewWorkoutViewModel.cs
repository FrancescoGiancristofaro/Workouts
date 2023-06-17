using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkoutsApp.Extensions;
using WorkoutsApp.Models.DB;
using WorkoutsApp.Models.Dtos;
using WorkoutsApp.Services;

namespace WorkoutsApp.Pages.Workouts
{
    [QueryProperty(nameof(SelectedExercises), "exercises")]
    public partial class AddNewWorkoutViewModel : BaseViewModel
    {
        public ObservableCollection<SelectableExerciseDto> SelectedExercises { get; set; }

        private readonly IPopupService _popupService;
        private readonly IExerciseService _exerciseService;

        [ObservableProperty] string _name;
        public ObservableCollection<SelectableExerciseDto> ExercisesList { get; set; } = new ();

        [RelayCommand]
        async void SelectExercise()
        {
            await Shell.Current.GoToAsync(AppRoutes.SelectExercisesPage,"exercises", ExercisesList);
        }

        [RelayCommand]
        async void OpenAddSeriesPopup(SelectableExerciseDto exercise)
        {
            var res = await _popupService.ShowPopup(typeof(AddSeriesPage), exercise.Series.LastOrDefault());
            if(res is SeriesDto series)
                exercise.Series.Add(series);
        }

        [RelayCommand]
        async void DeleteSeries(object parameters)
        {
            var res = await Shell.Current.DisplayAlert("Attenzione","Sei sicuro di voler eliminare la serie", "Ok", "Annulla");
            if(!res)
                return;
            var tuple = parameters as Tuple<SeriesDto, SelectableExerciseDto>;
            tuple.Item2.Series.Remove(tuple.Item1);
        }


        public AddNewWorkoutViewModel(IPopupService popupService,IExerciseService exerciseService)
        {
            _popupService = popupService;
            _exerciseService = exerciseService;
        }

        protected override async Task Appearing()
        {
            IsBusy = true;
            if (SelectedExercises.SafeAny())
            {
                foreach (var selectableExerciseDto in SelectedExercises)
                {
                    var item = ExercisesList.FirstOrDefault(x => x.Exercise.Id == selectableExerciseDto.Exercise.Id);
                    if (selectableExerciseDto.IsSelected && item is null)
                    {
                        ExercisesList.Add(selectableExerciseDto);
                    }
                    if(!selectableExerciseDto.IsSelected && item is not null)
                    {
                        ExercisesList.Remove(item);
                    }
                }
            }
            IsBusy = false;
        }
    }
}
