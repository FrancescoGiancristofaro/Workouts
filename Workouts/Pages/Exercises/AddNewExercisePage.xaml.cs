namespace WorkoutsApp.Pages.Exercises;

public partial class AddNewExercisePage
{
	public AddNewExercisePage(AddNewExerciseViewModel vm)
    {
		BindingContext = vm;
		InitializeComponent();
	}
}