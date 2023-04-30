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
        private int _indexDragged;
        private SelectableExerciseDto _exerciseDragged;

        [ObservableProperty] string _name;
        [ObservableProperty] ObservableCollection<SelectableExerciseDto> _selectableExercises = new();

        [RelayCommand]
        async void SelectExercise()
        {
            var selectedExercises = new Dictionary<string, object>() { { "exercises", SelectableExercises } };
            await Shell.Current.GoToAsync("selectexercise", selectedExercises);
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

        [RelayCommand]
        void ItemDraggedOver(SelectableExerciseDto item)
        {

            //rimuovo draggato
            SelectableExercises.Remove(_exerciseDragged);

            var indexItemToInsertBefore = SelectableExercises.IndexOf(item);
            SelectableExercises.Insert(indexItemToInsertBefore, _exerciseDragged);

            var itemBeingDragged = SelectableExercises.FirstOrDefault(i => i.IsBeingDragged);

            foreach (var i in SelectableExercises)
            {
                i.IsBeingDraggedOver = item == i && item != itemBeingDragged;
            }
        }

        [RelayCommand]
        void ItemDragLeave(SelectableExerciseDto item)
        {

            foreach (var i in SelectableExercises)
            {
                i.IsBeingDraggedOver = false;
            }
        }

        [RelayCommand]
        void ItemDragged(SelectableExerciseDto item)
        {
            _indexDragged = SelectableExercises.IndexOf(item);
            _exerciseDragged = item;
            foreach (var i in SelectableExercises)
            {
                i.IsBeingDragged = item == i;
            }
        }

        [RelayCommand]
        async void ItemDropped(SelectableExerciseDto item)
        {
            //rimuovo draggato
            //SelectableExercises.Remove(_exerciseDragged);

            //var indexItemToInsertBefore = SelectableExercises.IndexOf(item);
            //SelectableExercises.Insert(indexItemToInsertBefore, _exerciseDragged);


            //// Wait for remove animation to be completed
            //// https://github.com/xamarin/Xamarin.Forms/issues/13791
            //await Task.Delay(1000);
            _exerciseDragged.IsBeingDragged = false;
            item.IsBeingDraggedOver = false;

        }

        //public void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        //{
        //    if (e.StatusType == GestureStatus.Running)
        //    {
        //        // Get the current item being dragged
        //        var currentItem = _exerciseDragged;

        //        // Get the position of the finger on the screen
        //        var position = e.TotalY;

        //        // Check if the finger is dragging the item above or below the current position
        //        if (position < 0 && _indexDragged < SelectableExercises.Count - 1)
        //        {
        //            // Move the item down in the list
        //            var newIndex = _indexDragged + 1;
        //            SelectableExercises.Remove(_exerciseDragged);
        //            SelectableExercises.Insert(newIndex, currentItem);
        //            _indexDragged = newIndex;
        //        }
        //        else if (position > 0 && _indexDragged > 0)
        //        {
        //            // Move the item up in the list
        //            var newIndex = _indexDragged - 1;
        //            SelectableExercises.RemoveAt(_indexDragged);
        //            SelectableExercises.Insert(newIndex, currentItem);
        //            _indexDragged = newIndex;
        //        }
        //    }
        //}

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
            if (query.ContainsKey("exercises") && query["exercises"] is ObservableCollection<SelectableExerciseDto> exercisesInput)
            {
                SelectableExercises = new ObservableCollection<SelectableExerciseDto>(exercisesInput.Where(x=>x.IsSelected));
            }
        }
    }
}
