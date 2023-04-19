namespace WorkoutsApp.Pages.Exercises;

public partial class ExercisesListPage
{
	public ExercisesListPage(ExercisesListViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}