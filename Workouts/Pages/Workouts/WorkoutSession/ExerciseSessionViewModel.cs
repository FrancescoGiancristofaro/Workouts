using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Plugin.Maui.Audio;
using Services.Constants;
using Services.Dtos;
using Services.Services;
using WorkoutsApp.Dtos;

namespace WorkoutsApp.Pages.Workouts.WorkoutSession
{
    [QueryProperty(nameof(WorkoutExerciseDetailsList), "exsessionlist")]

    public partial class ExerciseSessionViewModel : BaseViewModel
    {
        private readonly IExerciseService _exerciseService;
        private readonly IAudioManager _audioManager;

        public List<SelectableWorkoutsExerciseDetailsDto> WorkoutExerciseDetailsList { get; set; }
        [ObservableProperty] ExerciseDto _exerciseDto;
        [ObservableProperty] private SelectableWorkoutsExerciseDetailsDto _currentWorkoutExerciseDetails;
        [ObservableProperty] private bool _isLastExercise;
        [ObservableProperty] private ObservableCollection<ExecutableSeriesDto> _series;

        public ExerciseSessionViewModel(IExerciseService exerciseService,IAudioManager audioManager)
        {
            _exerciseService = exerciseService;
            _audioManager = audioManager;
            WeakReferenceMessenger.Default.Register<ValueChangedMessage<RecoveryStatus>>(this, async (r, m) =>
            {
                await PlayAlertSound();
            });
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task StartTimer(ExecutableSeriesDto dto)
        {
            try
            {
                foreach (var item in Series)
                {
                    item.StopTimer();
                }
                dto.StartTimer();
            }
            catch (Exception ex)
            {
                await ManageException(ex);
            }
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task ResumeTimer(ExecutableSeriesDto dto)
        {
            try
            {
                dto.StartTimer(true);
            }
            catch (Exception ex)
            {
                await ManageException(ex);
            }
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task StopTimer(ExecutableSeriesDto dto)
        {
            try
            {
                dto.StopTimer();
            }
            catch (Exception ex)
            {
                await ManageException(ex);
            }
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task PauseTimer(ExecutableSeriesDto dto)
        {
            try
            {
                dto.PauseTimer();
            }
            catch (Exception ex)
            {
                await ManageException(ex);
            }
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task Next()
        {
            try
            {
                IsBusy = true;


                if (IsLastExercise)
                {
                    await Shell.Current.DisplayAlert("Attenzione", "Funzionalita in arrivo", "OK");
                    return;
                }

                var index = WorkoutExerciseDetailsList.IndexOf(CurrentWorkoutExerciseDetails);
                CurrentWorkoutExerciseDetails.IsSelected = false;
                WorkoutExerciseDetailsList[index + 1].IsSelected = true;


                await GoToAsync(AppRoutes.ExerciseSessionPage, "exsessionlist", WorkoutExerciseDetailsList);
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

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task Back()
        {
            await NavigateBack();
        }
        public override async void PrepareModel()
        {
            IsBusy = true;
            try
            {
                var exercise = WorkoutExerciseDetailsList.FirstOrDefault(x => x.IsSelected);
                CurrentWorkoutExerciseDetails = exercise is null ? WorkoutExerciseDetailsList.First() : exercise;
                IsLastExercise = CurrentWorkoutExerciseDetails.Equals(WorkoutExerciseDetailsList.LastOrDefault());
                var series = await _exerciseService.GetSeriesByExerciseDetailAsync(CurrentWorkoutExerciseDetails.ExerciseDetail.Id,
                    CurrentWorkoutExerciseDetails.ExerciseDetail.IdWorkout);
                
                Series = new ObservableCollection<ExecutableSeriesDto>(series.Select(x=>new ExecutableSeriesDto()
                {
                    Series=x,
                    RecoveryStatus = RecoveryStatus.Stopped
                }));

                ExerciseDto = await _exerciseService.GetExerciseByIdAsync(CurrentWorkoutExerciseDetails.ExerciseDetail.IdExercise);
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
                if (!CurrentWorkoutExerciseDetails.Equals(WorkoutExerciseDetailsList.First()))
                {
                    var index = WorkoutExerciseDetailsList.IndexOf(CurrentWorkoutExerciseDetails);
                    WorkoutExerciseDetailsList[index].IsSelected= false;
                    WorkoutExerciseDetailsList[index-1].IsSelected=true;
                    await GoToAsync("..", "exsessionlist", WorkoutExerciseDetailsList);
                    return;
                }
                await base.NavigateBack();
            }
            catch (Exception ex)
            {
                await ManageException(ex);
            }
        }

        private async Task PlayAlertSound()
        {
            try
            {
                var file = await FileSystem.OpenAppPackageFileAsync("alert_end_recovery.mp3");
                using var player = _audioManager.CreatePlayer(file);
                player.Play();
            }
            catch (Exception ex)
            {
                await ManageException(ex);
            }
        }
    }
}
