namespace WorkoutsApp.Pages.Templates.Components;

public partial class DisableControlComponent : ContentView
{
    public static readonly BindableProperty IsDisabledProperty = BindableProperty.Create(nameof(IsDisabled), typeof(bool), typeof(DisableControlTemplate), false);

    public bool IsDisabled
    {
        get => (bool)GetValue(IsDisabledProperty);
        set => SetValue(IsDisabledProperty, value);
    }

    public DisableControlComponent()
	{
		InitializeComponent();
	}
}