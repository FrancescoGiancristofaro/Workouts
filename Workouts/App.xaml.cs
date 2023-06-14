#if __ANDROID__
using Android.Graphics.Drawables;
#endif
namespace WorkoutsApp;
public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        MainPage = new AppShell();
        RegisterAndroidHandlers();
    }

    protected override void OnStart()
    {
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
    }

    private void RegisterAndroidHandlers()
    {
        Microsoft.Maui.Handlers.ElementHandler.ElementMapper.AppendToMapping("CornerRadius", (handler, view) =>
        {
#if __ANDROID__
            if (handler is CommunityToolkit.Maui.Core.Handlers.PopupHandler popupHandler)
            {
                var popup = (Android.App.Dialog)handler.PlatformView;
                var drawable = new GradientDrawable();
                drawable.SetShape(ShapeType.Rectangle);
                drawable.SetCornerRadii(new float[] { 25, 25, 25, 25, 0, 0, 0, 0 });
                drawable.SetPadding(10, 10, 10, 10);
                popup.Window.SetBackgroundDrawable(drawable);
            }
#endif
        });

    }

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        ((IBaseViewModel)Shell.Current.CurrentPage.BindingContext).ManageException(e);
    }
}
