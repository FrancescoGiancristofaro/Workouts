namespace WorkoutsApp.Pages.Workouts;

public partial class AddSeriesPopup
{
	public AddSeriesPopup(AddSeriesViewModel vm)
	{
		vm.Popup = this;
		BindingContext = vm;
		InitializeComponent();
	}
}