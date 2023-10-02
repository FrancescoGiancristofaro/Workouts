using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutsApp.Pages.Exercises;
using WorkoutsApp.Pages.Schedules;
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
        public const string AddScheduledWorkoutPage = "addscheduledworkout";
        public static void RegisterRoutes()
        {
            Routing.RegisterRoute(AddNewExercisePage, typeof(AddNewExercisePage));
            Routing.RegisterRoute(AddNewWorkoutPage, typeof(AddNewWorkoutPage));
            Routing.RegisterRoute(SelectExercisesPage, typeof(SelectExercisesPage));
            Routing.RegisterRoute(ExerciseConfigurationPage, typeof(ExerciseConfigurationPage));
            Routing.RegisterRoute(WorkoutDetailPage, typeof(WorkoutDetailsPage));
            Routing.RegisterRoute(ExerciseSessionPage, typeof(ExerciseSessionPage));
            Routing.RegisterRoute(AddScheduledWorkoutPage, typeof(AddScheduledWorkoutPage));
        }
    }
}
