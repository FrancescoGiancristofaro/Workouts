namespace WorkoutsApp.Pages.Workouts.WorkoutSession;

public partial class ExerciseSessionPage
{
	public ExerciseSessionPage(ExerciseSessionViewModel vm)
    {
        BindingContext = vm;
		InitializeComponent();
	}
}