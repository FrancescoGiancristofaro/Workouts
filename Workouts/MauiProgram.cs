using AutoMapper;
using CommunityToolkit.Maui;
using Plugin.Maui.Audio;
using Repositories.Repositories;
using Repositories.Settings;
using Services.Automapper;
using Services.Services;
using Syncfusion.Maui.Core.Hosting;
using WorkoutsApp.Pages.Exercises;
using WorkoutsApp.Pages.Schedules;
using WorkoutsApp.Pages.Templates;
using WorkoutsApp.Pages.Workouts;
using WorkoutsApp.Pages.Workouts.Wizard;
using WorkoutsApp.Pages.Workouts.WorkoutSession;


namespace WorkoutsApp;

public static class MauiProgram
{
    public static Task InitialStartupTask;

	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
            .ConfigureSyncfusionCore()
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
        RegisterHandlers(builder);

        AppRoutes.RegisterRoutes();

        var serviceProvider = builder.Services.BuildServiceProvider();
        serviceProvider.GetRequiredService<ICacheService>().SetScope("Workouts");

        InitialStartupTask = serviceProvider.GetRequiredService<IMigrationService>().Migrate();

        return builder.Build();
	}

    private static void RegisterHandlers(MauiAppBuilder builder)
    {
        //builder.Services.ConfigureMauiHandlers(handlers =>
        //{
        //    //handlers.AddHandler(typeof(BasePopup), typeof(PopupHandler));
        //});
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
        serviceCollection.AddTransient<ISeriesHistoryRepository, SeriesHistoryRepository>();
        serviceCollection.AddTransient<IWorkoutSessionRepository, WorkoutSessionRepository>();
        serviceCollection.AddTransient<IWorkoutsScheduledRepository, WorkoutsScheduledRepository>();
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
        serviceCollection.AddTransient<InfoPopupViewModel>();
        serviceCollection.AddTransient<EditorPopupViewModel>();
        serviceCollection.AddTransient<AddScheduledWorkoutViewModel>();
        serviceCollection.AddTransient<WorkoutSessionDetailViewModel>();
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
        serviceCollection.AddTransient<InfoPopup>();
        serviceCollection.AddTransient<EditorPopup>();
        serviceCollection.AddTransient<ExerciseConfigurationPage>();
        serviceCollection.AddTransient<WorkoutDetailsPage>();
        serviceCollection.AddTransient<ExerciseSessionPage>();
        serviceCollection.AddTransient<AddScheduledWorkoutPage>();
        serviceCollection.AddTransient<WorkoutSessionDetailPopup>();
    }
    
}
