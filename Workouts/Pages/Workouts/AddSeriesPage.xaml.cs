namespace WorkoutsApp.Pages.Workouts;

public partial class AddSeriesPage
{
	public AddSeriesPage(AddSeriesViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}