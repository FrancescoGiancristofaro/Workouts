using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repositories.Models;
using Services.Constants;
using Services.Dtos;
using Services.Services;

namespace WorkoutsApp.Pages.Exercises
{
    public partial class AddNewExerciseViewModel : BaseViewModel
    {
        private readonly IExerciseService _exerciseService;
        [ObservableProperty] string _exerciseName;
        [ObservableProperty] string _description = string.Empty;
        [ObservableProperty] string _selectedCategory;
        [ObservableProperty] IList<string> _categories;

        [RelayCommand(AllowConcurrentExecutions = false)]
        async Task SaveExercise()
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

                var ex = new ExerciseDto() { Name = ExerciseName, Description = Description, Category = category };
                await _exerciseService.InsertExerciseAsync(ex);

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

        public override void PrepareModel()
        {
            Categories = Enum.GetValues(typeof(ExerciseCategory)).Cast<ExerciseCategory>().Select(x=>x.ToString()).ToList();
            SelectedCategory = Categories.First();
        }

        public AddNewExerciseViewModel(IExerciseService exerciseService, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _exerciseService = exerciseService;
        }

    }
}
