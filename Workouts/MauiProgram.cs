﻿using CommunityToolkit.Maui;
using WorkoutsApp.Pages.Exercises;
using WorkoutsApp.Pages.Workouts;
using WorkoutsApp.Repositories;
using WorkoutsApp.Services;
using WorkoutsApp.Settings;

namespace WorkoutsApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("Brands-Regular-400.otf", "FAB");
				fonts.AddFont("Free-Regular-400.otf", "FAR");
				fonts.AddFont("Free-Solid-900.otf", "FAS");
            });

        builder.Services.AddTransient<AppShell>();
        RegisterDatabase(builder.Services);
        RegisterServices(builder.Services);
        RegisterRepositories(builder.Services);
        RegisterViewModels(builder.Services);
        RegisterPages(builder.Services);

        RegisterRoutes();

        return builder.Build();
	}

    private static void RegisterDatabase(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<DBManager>();
    }
    private static void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IWorkoutService, WorkoutService>();
        serviceCollection.AddTransient<IExerciseService, ExerciseService>();
    }
    private static void RegisterRepositories(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IWorkoutsRepository, WorkoutsRepository>();
        serviceCollection.AddTransient<IExerciseRepository, ExerciseRepository>();
    }
    private static void RegisterViewModels(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<WorkoutsViewModel>();
        serviceCollection.AddTransient<ExercisesListViewModel>();
        serviceCollection.AddTransient<AddNewExerciseViewModel>();
    }
    private static void RegisterPages(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<WorkoutsPage>();
        serviceCollection.AddTransient<AddNewExercisePage>();
        serviceCollection.AddTransient<ExercisesListPage>();
    }

    private static void RegisterRoutes()
    {
        Routing.RegisterRoute("addnewexercise", typeof(AddNewExercisePage));
    }
}
