namespace WorkoutsApp.Pages.Schedules;

public partial class AddScheduledWorkoutPage
{
	public AddScheduledWorkoutPage(AddScheduledWorkoutViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}

}