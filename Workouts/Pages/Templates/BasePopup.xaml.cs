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