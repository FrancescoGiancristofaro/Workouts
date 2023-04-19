using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutsApp.Constants;
using WorkoutsApp.Extensions;
using WorkoutsApp.Models.DB;
using WorkoutsApp.Models.Dtos;
using WorkoutsApp.Services;

namespace WorkoutsApp.Pages.Exercises
{
    public partial class ExercisesListViewModel : BaseViewModel
    {
        private readonly IExerciseService _exerciseService;

        public IEnumerable<ExerciseCategory> Categories => Enum.GetValues(typeof(ExerciseCategory)).Cast<ExerciseCategory>();

        [ObservableProperty] ExerciseCategory _selectedCategory;

        [ObservableProperty] ObservableCollection<Exercise> _exercises;

        [ObservableProperty] bool _isExercisesListEmpty;
        [ObservableProperty] string _textToSearch = string.Empty;

        [RelayCommand]
        public async void AddNewExercise()
        {
            await Shell.Current.GoToAsync("addnewexercise");
        }

        [RelayCommand]
        public async void Filter()
        {
            var filters = new ExerciseFiltersDto() { TextToSearch = TextToSearch, Category = SelectedCategory };
            await RefreshExercisesList(filters);
        }

        [RelayCommand]
        public async void DeleteExercise(object data)
        {
            try
            {
                var result = await Shell.Current.DisplayAlert("Attenzione", "Sei sicuro di voler eliminare l'esericio? Operazione irreversibile", "OK","Annulla");
                if(!result)
                    return;
                IsBusy = true;
                var ex = await _exerciseService.GetById((int)data);
                await _exerciseService.Delete(ex);
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

        public ExercisesListViewModel(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        protected override async void Appearing()
        {
            IsBusy = true;
            try
            {
                await RefreshExercisesList();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task RefreshExercisesList(ExerciseFiltersDto filters = null)
        {
            Exercises = new ObservableCollection<Exercise>();
            var ex = await _exerciseService.GetAll();

            if (filters == null)
            {
                foreach (var item in ex)
                {
                    Exercises.Add(item);
                }
            }
            else
            {
                var filteredEx = ex.Where(ex => (filters.Category is ExerciseCategory.NoCategory || ex.Category == filters.Category) &&
                      (ex.Description.Contains(filters.TextToSearch) ||
                       ex.Name.Contains(filters.TextToSearch)));
                foreach (var item in filteredEx)
                {
                    Exercises.Add(item);
                }
            }

            IsExercisesListEmpty = !Exercises.SafeAny();

        }
    }
}
