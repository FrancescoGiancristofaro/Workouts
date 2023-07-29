using Services.Services;

namespace WorkoutsApp;
public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        MainPage = new AppShell();
    }

    protected override async void OnStart()
    { 
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        await InitDatabase();
    }

    private async Task InitDatabase()
    {
        var service = MauiApplication.Current.Services.GetRequiredService<IMigrationService>();
        await service.Migrate();
    }

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        ((IBaseViewModel)Shell.Current.CurrentPage.BindingContext).ManageException(e);
    }
}
