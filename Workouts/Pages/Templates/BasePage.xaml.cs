namespace WorkoutsApp.Pages.Templates;

public partial class BasePage : ContentPage
{
    public static readonly BindableProperty RootViewModelProperty =
        BindableProperty.Create(
            "RootViewModel", typeof(BaseViewModel), typeof(BasePage),
            defaultValue: default(BaseViewModel));

    public BaseViewModel RootViewModel
    {
        get => (BaseViewModel)GetValue(RootViewModelProperty);
        set => SetValue(RootViewModelProperty, value);
    }
    public BasePage()
	{
		InitializeComponent();
	}

    protected override bool OnBackButtonPressed()
    {
        //if (RootViewModel.IsBusy)
        //    return true;
        //RootViewModel.NavigateBack();
        return true;
    }
}