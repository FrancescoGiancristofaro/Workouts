#if __ANDROID__
using Android.Graphics.Drawables;
#endif
using CommunityToolkit.Maui.Core;

namespace WorkoutsApp.Pages.Templates;

public partial class BasePopup 
{
    public static readonly BindableProperty RootViewModelProperty =
        BindableProperty.Create(
            "RootViewModel", typeof(BasePopupViewModel), typeof(BasePopup),
            defaultValue: default(BasePopupViewModel));

    public BasePopupViewModel RootViewModel
    {
        get => (BasePopupViewModel)GetValue(RootViewModelProperty);
        set => SetValue(RootViewModelProperty, value);
    }

    public object Data { get; set; }

    public BasePopup()
    {
        this.Closed += BasePopup_OnClosed;
        this.Opened += BasePopup_OnOpened;
        InitializeComponent();

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

    private void BasePopup_OnOpened(object sender, PopupOpenedEventArgs e)
    {
        RootViewModel.Opened();
    }

    private void BasePopup_OnClosed(object sender, PopupClosedEventArgs e)
    {
        this.Closed -= BasePopup_OnClosed;
        this.Opened -= BasePopup_OnOpened;
        RootViewModel.Dismissed();
    }
}