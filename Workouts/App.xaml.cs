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
        RegisterAndroidHandlers();
    }

    protected override void OnStart()
    { 
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
    }
    private void RegisterAndroidHandlers()
    {
//        CommunityToolkit.Maui.Core.Handlers.PopupHandler.PopUpMapper.AppendToMapping("CornerRadius", (handler, view) =>
//        {
//#if __ANDROID__
//                var popup = (Android.App.Dialog)handler.PlatformView;
//                var drawable = new GradientDrawable();
//                drawable.SetShape(ShapeType.Rectangle);
//                drawable.SetCornerRadii(new float[] { 25, 25, 25, 25, 0, 0, 0, 0 });
//                drawable.SetPadding(10, 10, 10, 10);
//                popup.Window.SetBackgroundDrawable(drawable);
            
//#endif
//        });

//        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("Focused", (handler, view) =>
//        {
//#if ANDROID
//            handler.PlatformView.FocusChange += (sender, args) =>
//            {
//                if (args.HasFocus)
//                {
//                    InputMethodManager inputMethodManager = GetSystemService(InputMethodService) as InputMethodManager;
//                    inputMethodManager?.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.None);

//                }

//            };
//#endif
//        });
    }

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        ((IBaseViewModel)Shell.Current.CurrentPage.BindingContext).ManageException(e);
    }
}
