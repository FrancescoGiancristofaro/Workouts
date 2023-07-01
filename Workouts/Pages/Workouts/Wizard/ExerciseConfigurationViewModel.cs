using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services.Dtos;
using WorkoutsApp.Dtos;
using WorkoutsApp.Extensions;

namespace WorkoutsApp.Pages.Workouts.Wizard
{
    
    [QueryProperty(nameof(ExerciseList), "exercises")]
    public partial class ExerciseConfigurationViewModel : BaseViewModel
    {
        public List<SelectableExerciseDto> ExerciseList { get; set; }
        [ObservableProperty] private SelectableExerciseDto _currentExercise;
        [ObservableProperty] private bool _isLastExercise;

        [RelayCommand]
        public async void Next()
        {
            if (IsLastExercise)
            {
                await Shell.Current.DisplayAlert("Attenzione", "Funzionalita in arrivo", "Ok");
                return;
            }
            var index = ExerciseList.IndexOf(CurrentExercise);
            CurrentExercise.IsSelected = false;
            ExerciseList[index+1].IsSelected = true;
            await Shell.Current.GoToAsync(AppRoutes.ExerciseConfigurationPage, "exercises", ExerciseList);

        }
        public override async void PrepareModel()
        {
            IsBusy = true;
            try
            {
                var exercise = ExerciseList.FirstOrDefault(x => x.IsSelected);
                CurrentExercise = exercise is null ? ExerciseList.First() : exercise;
                IsLastExercise = CurrentExercise.Equals(ExerciseList.LastOrDefault());
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

        public override async void NavigateBack()
        {
            try
            {
                var index = ExerciseList.IndexOf(CurrentExercise);
                if (index is 0)
                {
                    await Shell.Current.GoToAsync("..");
                    return;
                }

                CurrentExercise.IsSelected = false;
                ExerciseList[index - 1].IsSelected = true;
                await Shell.Current.GoToAsync("..", "exercises", ExerciseList);
            }
            catch (Exception ex)
            {
                await ManageException(ex);
            }
        }
    }
}
