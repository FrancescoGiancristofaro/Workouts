using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkoutsApp.Extensions;
using WorkoutsApp.Models.Dtos;
using WorkoutsApp.Services;

namespace WorkoutsApp.Pages.Workouts
{
    [QueryProperty(nameof(SelectedExercises),"exercises")]
    public partial class SelectExercisesViewModel : BaseViewModel
    {
        public ObservableCollection<SelectableExerciseDto> SelectedExercises { get; set; }

        private readonly IExerciseService _exerciseService;
        
        [ObservableProperty] bool _isExercisesListEmpty;
        [ObservableProperty] ObservableCollection<SelectableExerciseDto> _exercisesList = new();

        [RelayCommand]
        void SelectExercise(SelectableExerciseDto exercise)
        {
            exercise.IsSelected = !exercise.IsSelected;
        }

        [RelayCommand]
        async void AddSelectedExercises()
        {
            await Shell.Current.GoToAsync("..", "exercises", ExercisesList);
        }

        public SelectExercisesViewModel(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }
        protected override async Task Appearing()
        {
            IsBusy = true;
            try
            {
                await RefreshExercisesList();
            }
            catch (Exception ex)
            {
                await ManageException(ex);
            }
            finally
            {
                IsBusy = false;
            }

        }
        private async Task RefreshExercisesList()
        {
            var exercises = await _exerciseService.GetAll();
            foreach (var item in exercises)
            {
                ExercisesList.Add(new SelectableExerciseDto() { Exercise = item, IsSelected = SelectedExercises.SafeAny(x => x.Exercise.Id == item.Id) });
            }
        }
    }
}
