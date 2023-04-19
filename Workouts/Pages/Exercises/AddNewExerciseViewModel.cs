using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        [ObservableProperty] string _name;
        [ObservableProperty] string _description = string.Empty;
        [ObservableProperty] ExerciseCategory _selectedCategory;

        [RelayCommand]
        async void SaveExercise()
        {
            try
            {
                IsBusy = true;
                if (string.IsNullOrEmpty(Name) || SelectedCategory is ExerciseCategory.NoCategory)
                {
                    await Shell.Current.DisplayAlert("Attenzione", "Campi nome e categoria sono obbligatori", "OK");
                    return;
                }

                var ex = new Exercise() { Name = Name, Description = Description, Category = SelectedCategory };
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

        public IEnumerable<ExerciseCategory> Categories => Enum.GetValues(typeof(ExerciseCategory)).Cast<ExerciseCategory>();
        public AddNewExerciseViewModel(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }
    }
}
