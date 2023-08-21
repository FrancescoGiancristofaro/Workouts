using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Android.Views.Animations;
using Android.Views.InputMethods;

namespace WorkoutsApp;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    public MainActivity()
    {
        CommunityToolkit.Maui.Core.Handlers.PopupHandler.PopUpMapper.AppendToMapping("CornerRadius", (handler, view) =>
        {
            var popup = (Android.App.Dialog)handler.PlatformView;
            var drawable = new GradientDrawable();
            drawable.SetShape(ShapeType.Rectangle);
            drawable.SetCornerRadii(new float[] { 25, 25, 25, 25, 0, 0, 0, 0 });
            drawable.SetPadding(10, 10, 10, 10);
            popup.Window.SetBackgroundDrawable(drawable);

        });
        
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("Focused", (handler, view) =>
        {
            
            handler.PlatformView.FocusChange += (sender, args) =>
            {
                InputMethodManager inputMethodManager = GetSystemService(InputMethodService) as InputMethodManager;
                if (args.HasFocus)
                {
                    inputMethodManager?.ShowSoftInput(handler.PlatformView,ShowFlags.Forced);
                }
                else
                {
                    inputMethodManager?.HideSoftInputFromWindow(handler.PlatformView.WindowToken, HideSoftInputFlags.None);
                }

            };
        });
    }

    protected override void OnCreate(Bundle savedInstanceState)
    {
        Window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#32322C"));
        Window.SetNavigationBarColor(Android.Graphics.Color.ParseColor("#32322C"));
        base.OnCreate(savedInstanceState);
    }
}
