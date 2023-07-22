using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services.Constants;
using Services.Dtos;
using Services.Services;
using WorkoutsApp.Dtos;
using WorkoutsApp.Extensions;
using WorkoutsApp.Services;

namespace WorkoutsApp.Pages.Workouts.Wizard
{
    
    [QueryProperty(nameof(ExerciseList), "exercises")]
    public partial class ExerciseConfigurationViewModel : BaseViewModel
    {
        private readonly IPopupService _popupService;
        private readonly ICacheService _cacheService;
        private readonly IWorkoutService _workoutService;

        public List<SelectableExerciseDto> ExerciseList { get; set; }
        public WorkoutWizardDto WorkoutWizardDto { get; set; }
        [ObservableProperty] private SelectableExerciseDto _currentExercise;
        [ObservableProperty] private ObservableCollection<SeriesDto> _series = new();
        [ObservableProperty] private bool _isLastExercise;

        public ExerciseConfigurationViewModel(IPopupService popupService,ICacheService cacheService,IWorkoutService workoutService)
        {
            _popupService = popupService;
            _cacheService = cacheService;
            _workoutService = workoutService;
        }

        [RelayCommand]
        public async void Next()
        {
            try
            {
                IsBusy = true;
                if (IsLastExercise)
                {
                    await _workoutService.CreateWorkoutsAsync(WorkoutWizardDto);
                    _cacheService.Remove(CacheKeys.WorkoutWizardProgression);
                }

                var index = ExerciseList.IndexOf(CurrentExercise);
                CurrentExercise.IsSelected = false;
                ExerciseList[index + 1].IsSelected = true;

                _cacheService.Add(CacheKeys.WorkoutWizardProgression, WorkoutWizardDto);

                await Shell.Current.GoToAsync(AppRoutes.ExerciseConfigurationPage, "exercises", ExerciseList);
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

        [RelayCommand]
        async void OpenAddSeriesPopup()
        {
            var res = await _popupService.ShowPopup(typeof(AddSeriesPopup), Series.LastOrDefault());
            if (res is SeriesDto series)
            {
                Series.Add(series);
                WorkoutWizardDto.InsertSeries(CurrentExercise.Exercise,series);
            }
        }

        [RelayCommand]
        async void DeleteSeries(object series)
        {
            var res = await Shell.Current.DisplayAlert("Attenzione", "Sei sicuro di voler eliminare la serie", "Ok", "Annulla");
            if (!res)
                return;
            Series.Remove(series as SeriesDto);
            WorkoutWizardDto.DeleteSeries(CurrentExercise.Exercise,series as SeriesDto);
        }

        public override async void PrepareModel()
        {
            IsBusy = true;
            try
            {
                var exercise = ExerciseList.FirstOrDefault(x => x.IsSelected);
                CurrentExercise = exercise is null ? ExerciseList.First() : exercise;
                IsLastExercise = CurrentExercise.Equals(ExerciseList.LastOrDefault());
                WorkoutWizardDto = _cacheService.Get<WorkoutWizardDto>(CacheKeys.WorkoutWizardProgression);
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

        public override async Task NavigateBack()
        {
            try
            {
                foreach (var item in Series)
                {
                    WorkoutWizardDto.DeleteSeries(CurrentExercise.Exercise,item);
                }

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
