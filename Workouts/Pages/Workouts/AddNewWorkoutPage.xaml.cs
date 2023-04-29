namespace WorkoutsApp.Pages.Workouts;

public partial class AddNewWorkoutPage
{
	public AddNewWorkoutPage(AddNewWorkoutViewModel vm)
    {
        BindingContext = vm;
		InitializeComponent();
	}
    private void DragGestureRecognizer_DragStarting_1(object sender, DragStartingEventArgs e)
    {
        var a = 0;

    }

    private void DropGestureRecognizer_Drop_1(object sender, DropEventArgs e)
    {
        var a = 0;
    }
}