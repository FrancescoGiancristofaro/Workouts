namespace WorkoutsApp.Pages.Schedules;

public partial class WorkoutSessionDetailPopup
{
	public WorkoutSessionDetailPopup(WorkoutSessionDetailViewModel vm)
	{
		vm.Popup = this;
        BindingContext = vm;
		InitializeComponent();
	}
}