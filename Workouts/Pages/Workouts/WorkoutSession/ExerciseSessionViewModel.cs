using System;
using System.Collections.Concurrent;
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
using WorkoutsApp.Pages.Templates;

namespace WorkoutsApp.Pages.Workouts.WorkoutSession
{
    [QueryProperty(nameof(WorkoutExerciseDetailsList), "exsessionlist")]

    public partial class ExerciseSessionViewModel : BaseViewModel
    {
        private readonly IExerciseService _exerciseService;
        private readonly IAudioManager _audioManager;
        private readonly ICacheService _cacheService;
        private readonly IWorkoutService _workoutService;

        public List<SelectableWorkoutsExerciseDetailsDto> WorkoutExerciseDetailsList { get; set; }

        [ObservableProperty] ExerciseDto _exerciseDto;
        [ObservableProperty] private SelectableWorkoutsExerciseDetailsDto _currentWorkoutExerciseDetails;
        [ObservableProperty] private bool _isLastExercise;
        [ObservableProperty] private ObservableCollection<ExecutableSeriesDto> _series;
        [ObservableProperty] private ObservableCollection<SeriesHistoryGroupedDto> _seriesHistory;

        public ExerciseSessionViewModel(
            IExerciseService exerciseService,
            IAudioManager audioManager,
            ICacheService cacheService,
            IWorkoutService workoutService, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _exerciseService = exerciseService;
            _audioManager = audioManager;
            _cacheService = cacheService;
            _workoutService = workoutService;
            WeakReferenceMessenger.Default.Register<ValueChangedMessage<RecoveryStatus>>(this, async (r, m) =>
            {
                await PlayAlertSound();
            });
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task ShowExerciseInfo()
        {
            try
            {
                await DisplayAlert("Info", ExerciseDto.Description);
            }
            catch (Exception ex)
            {
                await ManageException(ex);
            }
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task EditNote()
        {
            try
            {
                CurrentWorkoutExerciseDetails.ExerciseDetail.Note = await ShowEditorPopup("Note");
            }
            catch (Exception ex)
            {
                await ManageException(ex);
            }
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
        public async Task AddSeries()
        {
            try
            {
                var lastSeries = Series.LastOrDefault();
                if(lastSeries == null)
                {
                    Series.Add(new ExecutableSeriesDto()
                    {
                        RecoveryStatus = RecoveryStatus.Stopped,
                        SecondsLeft = 0,
                        Series = new SeriesDto()
                        {
                            Weight = 0,
                            SecondsRecoveryTime = 0,
                            Repetitions = 0
                        }
                    });
                    return;
                }
                Series.Add(new ExecutableSeriesDto()
                {
                    RecoveryStatus = RecoveryStatus.Stopped,
                    SecondsLeft = lastSeries.SecondsLeft,
                    Series = new SeriesDto()
                    {
                        Weight = lastSeries.Series.Weight,
                        SecondsRecoveryTime = lastSeries.Series.SecondsRecoveryTime,
                        Repetitions = lastSeries.Series.Repetitions
                    }
                });
            }
            catch (Exception ex)
            {
                await ManageException(ex);
            }
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task DeleteSeries(ExecutableSeriesDto dto)
        {
            try
            {
                Series.Remove(dto);
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

                var cachedSessionWorkout = _cacheService.Get<WorkoutSessionWizardDto>(CacheKeys.WorkoutSessionProgression);
                cachedSessionWorkout.InsertExercise(CurrentWorkoutExerciseDetails.ExerciseDetail, Series.Select(x=>x.Series));
                _cacheService.Add(CacheKeys.WorkoutSessionProgression, cachedSessionWorkout);

                if (IsLastExercise)
                {
                    await _workoutService.CreateWorkoutSessionAsync(cachedSessionWorkout);
                    await Shell.Current.Navigation.PopToRootAsync();
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
                var series = await _exerciseService.GetSeriesByExerciseDetailAsync(CurrentWorkoutExerciseDetails.ExerciseDetail.Id);
                
                Series = new ObservableCollection<ExecutableSeriesDto>(series.Select(x=>new ExecutableSeriesDto()
                {
                    Series=x,
                    RecoveryStatus = RecoveryStatus.Stopped
                }));

                ExerciseDto = await _exerciseService.GetExerciseByIdAsync(CurrentWorkoutExerciseDetails.ExerciseDetail.IdExercise);

                await LoadHistoryData();

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
                using var file = await FileSystem.OpenAppPackageFileAsync("alert_end_recovery.mp3");
                var player = _audioManager.CreatePlayer(file);
                player.Play();
            }
            catch (Exception ex)
            {
                await ManageException(ex);
            }
        }

        private async Task LoadHistoryData()
        {
            try
            {
                IsBusy = true;

                var history = await _exerciseService.GetSeriesHistoryByExerciseDetailAsync(CurrentWorkoutExerciseDetails.ExerciseDetail.IdExercise);
                var list = new ConcurrentBag<SeriesHistoryGroupedDto>();
                
                var opt = new ParallelOptions
                {
                    MaxDegreeOfParallelism = Environment.ProcessorCount
                };

                await Parallel.ForEachAsync(history.GroupBy(x => x.IdWorkoutSession), opt, async (item, cancellationToken) =>
                {
                    var workoutSessionInfo = await _workoutService.GetWorkoutBySessionIdAsync(item.Key);
                    var workout = await _workoutService.GetWorkoutByIdAsync(workoutSessionInfo.IdWorkout);
                    list.Add(new SeriesHistoryGroupedDto(item.Key, workoutSessionInfo.StartDate, workout.Name, item.ToList()));
                });

                SeriesHistory = new ObservableCollection<SeriesHistoryGroupedDto>(list.OrderByDescending(x=>x.IdWorkoutSession));
            }catch(Exception ex)
            {
                await ManageException(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
