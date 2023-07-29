using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Models;
using Services.Dtos;
using Services.Services;
using WorkoutsApp.Dtos;
using WorkoutsApp.Extensions;
using WorkoutsApp.Services;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Services.Constants;

namespace WorkoutsApp.Pages.Exercises
{
    public partial class ExercisesListViewModel : BaseViewModel
    {
        private readonly IExerciseService _exerciseService;

        public IEnumerable<ExerciseCategory> Categories => Enum.GetValues(typeof(ExerciseCategory)).Cast<ExerciseCategory>();

        [ObservableProperty] ExerciseCategory _selectedCategory;
        [ObservableProperty] ExerciseFiltersDto _filters;

        [ObservableProperty] ObservableCollection<ExerciseDto> _exercises;

        [ObservableProperty] bool _isExercisesListEmpty;
        [ObservableProperty] private bool _isRefreshing;
        [ObservableProperty] string _textToSearch = string.Empty;

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task AddNewExercise()
        {
            await Shell.Current.GoToAsync(AppRoutes.AddNewExercisePage);
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task RefreshExerciseList()
        {
            IsBusy = true;
            await RefreshExercisesList(Filters);
            IsRefreshing = false;
            IsBusy = false;
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task Filter()
        {
            Filters = new ExerciseFiltersDto() { TextToSearch = TextToSearch, Category = SelectedCategory };
            await RefreshExercisesList(Filters);
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task DeleteExercise(object data)
        {
            try
            {
                var result = await Shell.Current.DisplayAlert("Attenzione", "Sei sicuro di voler eliminare l'esericio? Operazione irreversibile", "OK","Annulla");
                if(!result)
                    return;
                IsBusy = true;
                await _exerciseService.DeleteExerciseByIdAsync((int)data);
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
            WeakReferenceMessenger.Default.Register<ValueChangedMessage<ExerciseOperation>>(this, async (r, m) =>
            {
                await RefreshExercisesList(Filters);
            });
        }

        public override async void OnAppearing()
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
            Exercises = new ObservableCollection<ExerciseDto>();
            var ex = await _exerciseService.GetExerciseListAsync();

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
