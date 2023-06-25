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
using Services.Services;
using WorkoutsApp.Dtos;
using WorkoutsApp.Extensions;
using WorkoutsApp.Models.Dtos;
using WorkoutsApp.Services;
using SelectableExerciseDto = WorkoutsApp.Dtos.SelectableExerciseDto;

namespace WorkoutsApp.Pages.Workouts
{
    [QueryProperty(nameof(SelectedExercises), "exercises")]
    public partial class AddNewWorkoutViewModel : BaseViewModel
    {
        public List<SelectableExerciseDto> SelectedExercises { get; set; }

        private readonly IPopupService _popupService;
        private readonly IExerciseService _exerciseService;

        [ObservableProperty] string _name;
        [ObservableProperty] ObservableCollection<ExercisesCategoryGroupedDto> _exercisesList = new();

        [RelayCommand]
        async void SelectExercise()
        {
            await Shell.Current.GoToAsync(AppRoutes.SelectExercisesPage, "exercises", ExercisesList.SelectMany(x => x.Select(z => z.Exercise.Id.Value)).ToList());
        }

        [RelayCommand]
        async void Next()
        {
            await Shell.Current.GoToAsync(AppRoutes.SelectExercisesPage, "exercises", ExercisesList);
        }

        //[RelayCommand]
        //async void OpenAddSeriesPopup(SelectableExerciseDto exercise)
        //{
        //    var res = await _popupService.ShowPopup(typeof(AddSeriesPopup), exercise.Series.LastOrDefault());
        //    if(res is SeriesDto series)
        //        exercise.Series.Add(series);
        //}

        //[RelayCommand]
        //async void DeleteSeries(object parameters)
        //{
        //    var res = await Shell.Current.DisplayAlert("Attenzione","Sei sicuro di voler eliminare la serie", "Ok", "Annulla");
        //    if(!res)
        //        return;
        //    var tuple = parameters as Tuple<SeriesDto, SelectableExerciseDto>;
        //    tuple.Item2.Series.Remove(tuple.Item1);
        //}


        public AddNewWorkoutViewModel(IPopupService popupService,IExerciseService exerciseService)
        {
            _popupService = popupService;
            _exerciseService = exerciseService;
        }

        protected override async Task Appearing()
        {
            IsBusy = true;
            var list = new ObservableCollection<ExercisesCategoryGroupedDto>();
            if (SelectedExercises.SafeAny())
            {
                foreach (var group in SelectedExercises.GroupBy(x=>x.Exercise.Category))
                   list.Add(new ExercisesCategoryGroupedDto(group.Key, group.ToList()));
            }
            ExercisesList = list;
            IsBusy = false;
        }
    }
}
