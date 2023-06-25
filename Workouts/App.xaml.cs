#if __ANDROID__
using Android.Content;
using Android.Graphics.Drawables;
using Android.InputMethodServices;
using Android.Views.InputMethods;
using Android.Widget;
#endif
namespace WorkoutsApp;
public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        MainPage = new AppShell();
    }

    protected override void OnStart()
    { 
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
    }

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        ((IBaseViewModel)Shell.Current.CurrentPage.BindingContext).ManageException(e);
    }
}
