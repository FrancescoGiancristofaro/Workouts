namespace WorkoutsApp.Pages.Schedules;

public partial class SchedulesPage 
{
	public SchedulesPage(SchedulesViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}

}