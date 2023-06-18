namespace WorkoutsApp.Pages.Workouts;

public partial class AddSeriesPopup
{
	public AddSeriesPopup(AddSeriesViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}