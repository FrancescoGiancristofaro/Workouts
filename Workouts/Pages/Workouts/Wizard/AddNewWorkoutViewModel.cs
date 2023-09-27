using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services.Constants;
using Services.Dtos;
using Services.Services;
using System.Collections.ObjectModel;
using WorkoutsApp.Dtos;
using WorkoutsApp.Extensions;
using SelectableExerciseDto = WorkoutsApp.Dtos.SelectableExerciseDto;

namespace WorkoutsApp.Pages.Workouts
{
    [QueryProperty(nameof(SelectedExercises), "exercises")]
    public partial class AddNewWorkoutViewModel : BaseViewModel
    {
        public List<SelectableExerciseDto> SelectedExercises { get; set; }

        private readonly IExerciseService _exerciseService;
        private readonly ICacheService _cacheService;

        [ObservableProperty] string _name;
        [ObservableProperty] string _description;
        [ObservableProperty] ObservableCollection<ExercisesCategoryGroupedDto> _exercisesList = new();

        public AddNewWorkoutViewModel(IExerciseService exerciseService, ICacheService cacheService, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _exerciseService = exerciseService;
            _cacheService = cacheService;
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        async Task SelectExercise()
        {
            await GoToAsync(AppRoutes.SelectExercisesPage, "exercises", ExercisesList.SelectMany(x => x.Select(z => z.Exercise.Id.Value)).ToList());
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        async Task Next()
        {
            if (string.IsNullOrEmpty(Name))
            {
                await Shell.Current.DisplayAlert("Attenzione", "Scegliere un nome", "Ok");
                return;
            }

            var copy = ExercisesList.SelectMany(x => x).Select(x => new SelectableExerciseDto()
            { IsSelected = false, Exercise = x.Exercise });

            var cachedWorkouts = WorkoutWizardDto.Create(Name, Description,copy.Select(x=>x.Exercise));
            
            _cacheService.Add(CacheKeys.WorkoutWizardProgression, cachedWorkouts);

            await GoToAsync(AppRoutes.ExerciseConfigurationPage, "exercises", copy.ToList());
        }


        public override async void ReversePrepareModel(object data = null)
        {
            try
            {
                IsBusy = true;
                await Task.Delay(1);
                var list = new ObservableCollection<ExercisesCategoryGroupedDto>();
                if (SelectedExercises.SafeAny())
                {
                    foreach (var group in SelectedExercises.GroupBy(x => x.Exercise.Category))
                        list.Add(new ExercisesCategoryGroupedDto(group.Key, group.ToList()));
                }

                ExercisesList = list;
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
            _cacheService.Remove(CacheKeys.WorkoutWizardProgression);
            await base.NavigateBack();
        }
    }
}
