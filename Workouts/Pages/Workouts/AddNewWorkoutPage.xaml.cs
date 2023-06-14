using WorkoutsApp.Models.Dtos;

namespace WorkoutsApp.Pages.Workouts;

public partial class AddNewWorkoutPage
{
    public AddNewWorkoutPage(AddNewWorkoutViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }

}