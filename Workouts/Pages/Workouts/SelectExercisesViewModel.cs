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
    public partial class SelectExercisesViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IExerciseService _exerciseService;
        [ObservableProperty] bool _isExercisesListEmpty;
        [ObservableProperty] ObservableCollection<SelectableExerciseDto> _selectableExercises = new ();
        private IEnumerable<SelectableExerciseDto> _selectedExercises = Enumerable.Empty<SelectableExerciseDto>();

        [RelayCommand]
        void SelectExercise(SelectableExerciseDto exercise)
        {
            exercise.IsSelected = !exercise.IsSelected;
        }

        [RelayCommand]
        async void AddSelectedExercises()
        {
            var returnValue = new Dictionary<string, object>() { { "exercises", SelectableExercises.Where(x=>x.IsSelected) } };
            await Shell.Current.GoToAsync("..",returnValue);

        }

        public SelectExercisesViewModel(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }
        protected override async void Appearing()
        {
            IsBusy = true;
            try
            {
                if (!SelectableExercises.SafeAny())
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

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("exercises") && query["exercises"] is IEnumerable<SelectableExerciseDto> selectedExercises)
            {
                _selectedExercises = selectedExercises;
            }
        }

        private async Task RefreshExercisesList()
        {
            SelectableExercises = new ObservableCollection<SelectableExerciseDto>();
            var exercises = await _exerciseService.GetAll();
            foreach (var item in exercises)
            {
                SelectableExercises.Add(new SelectableExerciseDto() { Exercise = item, IsSelected = _selectedExercises.Any(x => x.Exercise.Id == item.Id) });
            }
        }
    }
}
