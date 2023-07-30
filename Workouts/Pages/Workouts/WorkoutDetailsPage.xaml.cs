namespace WorkoutsApp.Pages.Workouts;

public partial class WorkoutDetailsPage
{
	public WorkoutDetailsPage(WorkoutDetailsViewModel vm)
    {
        BindingContext = vm;
		InitializeComponent();
	}
}