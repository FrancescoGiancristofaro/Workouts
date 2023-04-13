namespace WorkoutsApp.Pages.Workouts;

public partial class WorkoutsPage
{

    public WorkoutsPage(WorkoutsViewModel vm)
	{
        BindingContext = vm;
		InitializeComponent();
    }
}

