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
    public partial class AddNewWorkoutViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IPopupService _popupService;
        private readonly IExerciseService _exerciseService;

        [ObservableProperty] string _name;
        [ObservableProperty] ObservableCollection<SelectableExerciseDto> _selectableExercises = new();

        [RelayCommand]
        async void SelectExercise()
        {
            var selectedExercises = new Dictionary<string, object>() { { "exercises", SelectableExercises } };
            await Shell.Current.GoToAsync(AppRoutes.SelectExercisesPage,selectedExercises);
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

        protected override void Appearing()
        {
            
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("exercises") && query["exercises"] is IEnumerable<SelectableExerciseDto> exercisesInput)
            {
                SelectableExercises = new ObservableCollection<SelectableExerciseDto>(exercisesInput);
            }
        }
    }
}
