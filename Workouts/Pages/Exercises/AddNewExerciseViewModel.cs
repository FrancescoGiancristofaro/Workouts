using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkoutsApp.Constants;
using WorkoutsApp.Models.DB;
using WorkoutsApp.Services;

namespace WorkoutsApp.Pages.Exercises
{
    public partial class AddNewExerciseViewModel : BaseViewModel
    {
        private readonly IExerciseService _exerciseService;
        [ObservableProperty] string _exerciseName;
        [ObservableProperty] string _description = string.Empty;
        [ObservableProperty] string _selectedCategory;
        [ObservableProperty] IList<string> _categories;

        [RelayCommand]
        async void SaveExercise()
        {
            try
            {
                IsBusy = true;
                var parseRes = Enum.TryParse(SelectedCategory, out ExerciseCategory category);
                if (string.IsNullOrEmpty(ExerciseName) || !parseRes || category is ExerciseCategory.NoCategory)
                {
                    await Shell.Current.DisplayAlert("Attenzione", "Campi nome e categoria sono obbligatori", "OK");
                    return;
                }

                var ex = new Exercise() { Name = ExerciseName, Description = Description, Category = category };
                await _exerciseService.Insert(ex);

                await Shell.Current.GoToAsync("..");

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

        protected override async Task Appearing()
        {
            Categories = Enum.GetValues(typeof(ExerciseCategory)).Cast<ExerciseCategory>().Select(x=>x.ToString()).ToList();
        }

        public AddNewExerciseViewModel(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

    }
}
