using CommunityToolkit.Maui;
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
        RegistePages(builder.Services);
        return builder.Build();
	}

    public static void RegisterDatabase(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<DBManager>();
    }
    public static void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IWorkoutService, WorkoutService>();
    }
    public static void RegisterRepositories(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IWorkoutsRepository, WorkoutsRepository>();
    }
    public static void RegisterViewModels(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<WorkoutsViewModel>();
    }
    public static void RegistePages(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<WorkoutsPage>();
    }
}
