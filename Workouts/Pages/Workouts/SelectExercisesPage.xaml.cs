namespace WorkoutsApp.Pages.Workouts;

public partial class SelectExercisesPage 
{
	public SelectExercisesPage(SelectExercisesViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}