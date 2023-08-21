using AutoMapper;
using CommunityToolkit.Maui;
using Plugin.Maui.Audio;
using Repositories.Repositories;
using Repositories.Settings;
using Services.Automapper;
using Services.Services;
using WorkoutsApp.Pages.Exercises;
using WorkoutsApp.Pages.Schedules;
using WorkoutsApp.Pages.Workouts;
using WorkoutsApp.Pages.Workouts.Wizard;
using WorkoutsApp.Pages.Workouts.WorkoutSession;
using WorkoutsApp.Services;


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
        builder.Services.AddSingleton(AudioManager.Current);

        RegisterAutomapper(builder.Services);
        RegisterServices(builder.Services);
        RegisterRepositories(builder.Services);
        RegisterViewModels(builder.Services);
        RegisterPages(builder.Services);

        AppRoutes.RegisterRoutes();

        var cache = builder.Services.BuildServiceProvider().GetRequiredService<ICacheService>();
        cache.SetScope("Workouts");
        return builder.Build();
	}

    private static void RegisterAutomapper(IServiceCollection serviceCollection)
    {
        var config = new MapperConfiguration(c => {
            c.AddProfile<ExerciseProfile>();
            c.AddProfile<SeriesProfile>();
            c.AddProfile<WorkoutsProfile>();
        });
        serviceCollection.AddSingleton(s => config.CreateMapper());
    }
    private static void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IWorkoutService, WorkoutService>();
        serviceCollection.AddTransient<IExerciseService, ExerciseService>();
        serviceCollection.AddSingleton<IPopupService, PopupService>();
        serviceCollection.AddSingleton<IMigrationService, MigrationService>();
        serviceCollection.AddSingleton<ICacheService>(_ => new CacheService(FileSystem.Current.AppDataDirectory));
    }
    private static void RegisterRepositories(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IWorkoutsRepository, WorkoutsRepository>();
        serviceCollection.AddTransient<IExerciseRepository, ExerciseRepository>();
        serviceCollection.AddTransient<ISeriesRepository, SeriesRepository>();
        serviceCollection.AddTransient<IWorkoutExerciseDetailsRepository, WorkoutExerciseDetailsRepository>();
        serviceCollection.AddTransient<IMasterRepository, MasterRepository>();
    }
    private static void RegisterViewModels(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<WorkoutsViewModel>();
        serviceCollection.AddTransient<ExercisesListViewModel>();
        serviceCollection.AddTransient<AddNewExerciseViewModel>();
        serviceCollection.AddTransient<AddNewWorkoutViewModel>();
        serviceCollection.AddTransient<SelectExercisesViewModel>();
        serviceCollection.AddTransient<AddSeriesViewModel>();
        serviceCollection.AddTransient<ExerciseConfigurationViewModel>();
        serviceCollection.AddTransient<SchedulesViewModel>();
        serviceCollection.AddTransient<WorkoutDetailsViewModel>();
        serviceCollection.AddTransient<ExerciseSessionViewModel>();
    }
    private static void RegisterPages(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<SchedulesPage>();
        serviceCollection.AddTransient<WorkoutsPage>();
        serviceCollection.AddTransient<AddNewExercisePage>();
        serviceCollection.AddTransient<ExercisesListPage>();
        serviceCollection.AddTransient<SelectExercisesPage>();
        serviceCollection.AddTransient<AddNewWorkoutPage>();
        serviceCollection.AddTransient<AddSeriesPopup>();
        serviceCollection.AddTransient<ExerciseConfigurationPage>();
        serviceCollection.AddTransient<WorkoutDetailsPage>();
        serviceCollection.AddTransient<ExerciseSessionPage>();
    }
    
}
