namespace WorkoutsApp.Pages.Workouts.Wizard;

public partial class ExerciseConfigurationPage
{
    public ExerciseConfigurationPage(ExerciseConfigurationViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}