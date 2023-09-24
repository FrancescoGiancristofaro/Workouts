using Services.Services;

namespace WorkoutsApp;
public partial class App : Application
{
    private readonly IMigrationService _migrationService;

    public App(IMigrationService migrationService)
	{
		InitializeComponent();
        MainPage = new AppShell();
        _migrationService = migrationService;
    }

    protected override async void OnStart()
    { 
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        await InitDatabase();
    }

    private async Task InitDatabase()
    {
        await _migrationService.Migrate();
    }

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        ((IBaseViewModel)Shell.Current.CurrentPage.BindingContext).ManageException(e);
    }
}
