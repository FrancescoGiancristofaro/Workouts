using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutsApp.Pages.Exercises;
using WorkoutsApp.Pages.Workouts;

namespace WorkoutsApp
{
    public static class AppRoutes
    {
        public const string AddNewExercisePage = "addnewexercise";
        public const string AddNewWorkoutPage = "addnewworkout";
        public const string SelectExercisesPage = "selectexercise";
        public static void RegisterRoutes()
        {
            Routing.RegisterRoute("addnewexercise", typeof(AddNewExercisePage));
            Routing.RegisterRoute("addnewworkout", typeof(AddNewWorkoutPage));
            Routing.RegisterRoute("selectexercise", typeof(SelectExercisesPage));
        }
    }
}
