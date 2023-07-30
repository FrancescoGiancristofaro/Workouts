using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutsApp.Pages.Exercises;
using WorkoutsApp.Pages.Workouts;
using WorkoutsApp.Pages.Workouts.Wizard;
using WorkoutsApp.Pages.Workouts.WorkoutSession;

namespace WorkoutsApp
{
    public static class AppRoutes
    {
        public const string AddNewExercisePage = "addnewexercise";
        public const string AddNewWorkoutPage = "addnewworkout";
        public const string SelectExercisesPage = "selectexercise";
        public const string ExerciseConfigurationPage = "exerciseconfiguration";
        public const string WorkoutDetailPage = "workoutdetails";
        public const string ExerciseSessionPage = "exercisesession";
        public static void RegisterRoutes()
        {
            Routing.RegisterRoute("addnewexercise", typeof(AddNewExercisePage));
            Routing.RegisterRoute("addnewworkout", typeof(AddNewWorkoutPage));
            Routing.RegisterRoute("selectexercise", typeof(SelectExercisesPage));
            Routing.RegisterRoute("exerciseconfiguration", typeof(ExerciseConfigurationPage));
            Routing.RegisterRoute("workoutdetails", typeof(WorkoutDetailsPage));
            Routing.RegisterRoute("exercisesession", typeof(ExerciseSessionPage));
        }
    }
}
