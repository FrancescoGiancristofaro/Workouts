using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutsApp.Pages.Exercises;
using WorkoutsApp.Pages.Workouts;
using WorkoutsApp.Pages.Workouts.Wizard;

namespace WorkoutsApp
{
    public static class AppRoutes
    {
        public const string AddNewExercisePage = "addnewexercise";
        public const string AddNewWorkoutPage = "addnewworkout";
        public const string SelectExercisesPage = "selectexercise";
        public const string ExerciseConfigurationPage = "exerciseconfiguration";
        public static void RegisterRoutes()
        {
            Routing.RegisterRoute("addnewexercise", typeof(AddNewExercisePage));
            Routing.RegisterRoute("addnewworkout", typeof(AddNewWorkoutPage));
            Routing.RegisterRoute("selectexercise", typeof(SelectExercisesPage));
            Routing.RegisterRoute("exerciseconfiguration", typeof(ExerciseConfigurationPage));
        }
    }
}
