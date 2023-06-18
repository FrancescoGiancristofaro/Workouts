using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkoutsApp.Models.DB;
using WorkoutsApp.Services;

namespace WorkoutsApp.Pages.Exercises
{
    public partial class AddNewExerciseViewModel : BaseViewModel
    {
        private readonly IExerciseService _exerciseService;
        private readonly ICategoriesService _categoriesService;
        [ObservableProperty] string _exerciseName;
        [ObservableProperty] string _description = string.Empty;
        [ObservableProperty] ExerciseCategories _selectedCategory;
        [ObservableProperty] IEnumerable<ExerciseCategories> _categories;

        [RelayCommand]
        async void SaveExercise()
        {
            //try
            //{
            //    IsBusy = true;
            //    if (string.IsNullOrEmpty(Name) || SelectedCategory is ExerciseCategory.NoCategory)
            //    {
            //        await Shell.Current.DisplayAlert("Attenzione", "Campi nome e categoria sono obbligatori", "OK");
            //        return;
            //    }

            //    var ex = new Exercise() { Name = Name, Description = Description, Category = SelectedCategory };
            //    await _exerciseService.Insert(ex);

            //    await Shell.Current.GoToAsync("..");

            //}
            //catch (Exception ex)
            //{
            //    await ManageException(ex);
            //}
            //finally
            //{
            //    IsBusy = false;
            //}
        }

        protected override async Task Appearing()
        {
            Categories = await _categoriesService.GetAll();
        }

        public AddNewExerciseViewModel(IExerciseService exerciseService,ICategoriesService categoriesService)
        {
            _exerciseService = exerciseService;
            _categoriesService = categoriesService;
        }

    }
}
