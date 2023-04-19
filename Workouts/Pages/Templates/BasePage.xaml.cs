namespace WorkoutsApp.Pages.Templates;

public partial class BasePage : ContentPage
{
    public static readonly BindableProperty RootViewModelProperty =
        BindableProperty.Create(
            "RootViewModel", typeof(BaseViewModel), typeof(BasePage),
            defaultValue: default(BaseViewModel));

    public static readonly BindableProperty PageTitleProperty =
        BindableProperty.Create(
            "PageTitle", typeof(string), typeof(BasePage),
            defaultValue: default(string));

    public string PageTitle
    {
        get => (string)GetValue(PageTitleProperty);
        set => SetValue(PageTitleProperty, value);
    }
    public BaseViewModel RootViewModel
    {
        get => (BaseViewModel)GetValue(RootViewModelProperty);
        set => SetValue(RootViewModelProperty, value);
    }
    public BasePage()
	{
		InitializeComponent();
	}
}