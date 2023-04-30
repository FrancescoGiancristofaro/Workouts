namespace WorkoutsApp.Pages.Workouts;

public partial class AddNewWorkoutPage
{
	public AddNewWorkoutPage(AddNewWorkoutViewModel vm)
    {
        BindingContext = vm;
		InitializeComponent();
	}

    private void DragGestureRecognizer_DragStarting(Object sender, DragStartingEventArgs e)
    {
        var label = (Label)((Element)sender).Parent;

        e.Data.Properties["Label"] = label;

        //e.Handled = true;
    }

    private void DropGestureRecognizer_Drop(Object sender, DropEventArgs e)
    {
        var label = (Label)((Element)sender).Parent;
        var dropLabel = (Label)e.Data.Properties["Label"];
        if (label == dropLabel)
            return;


        var sourceContainer = (Grid)dropLabel.Parent;
        var targetContainer = (Grid)label.Parent;
        sourceContainer.Children.Remove(dropLabel);
        targetContainer.Children.Remove(label);
        sourceContainer.Children.Add(label);
        targetContainer.Children.Add(dropLabel);

        e.Handled = true;
    }

    private void DragGestureRecognizer_DragStarting_Collection(System.Object sender, DragStartingEventArgs e)
    {

    }

    private void DropGestureRecognizer_Drop_Collection(System.Object sender, DropEventArgs e)
    {
        // We handle reordering login in our view model
        e.Handled = true;
    }

}