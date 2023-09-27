using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;

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
    protected override async Task OnDismissedByTappingOutsideOfPopup()
    {
        this.Closed -= BasePopup_OnClosed;
        this.Opened -= BasePopup_OnOpened;
        MainThread.BeginInvokeOnMainThread(RootViewModel.Dismissed);
    }
    private void BasePopup_OnOpened(object sender, PopupOpenedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(RootViewModel.Opened);
    }

    private void BasePopup_OnClosed(object sender, PopupClosedEventArgs e)
    {
        this.Closed -= BasePopup_OnClosed;
        this.Opened -= BasePopup_OnOpened;
        MainThread.BeginInvokeOnMainThread(RootViewModel.Dismissed);
        MainThread.BeginInvokeOnMainThread(()=> { 
            (Shell.Current.Navigation.NavigationStack.Last().BindingContext as IBaseViewModel).ReversePrepareModel(e.Result);
        });
    }
}