namespace WorkoutsApp.Pages.Templates.Components;

public partial class WorkoutsEntry
{
    public static readonly BindableProperty TopLabelProperty =
        BindableProperty.Create(
            "TopLabel", typeof(string), typeof(WorkoutsEntry),
            defaultValue: default(string));
    public string TopLabel
    {
        get => (string)GetValue(TopLabelProperty);
        set => SetValue(TopLabelProperty, value);
    }

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(
            "Text", typeof(string), typeof(WorkoutsEntry),
            defaultValue: default(string),BindingMode.TwoWay);
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(
            "Placeholder", typeof(string), typeof(WorkoutsEntry),
            defaultValue: default(string));
    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public static readonly BindableProperty KeyboardProperty =
        BindableProperty.Create(
            "Keyboard", typeof(Keyboard), typeof(WorkoutsEntry),
            defaultValue: default(Keyboard));
    public Keyboard Keyboard
    {
        get => (Keyboard)GetValue(KeyboardProperty);
        set => SetValue(KeyboardProperty, value);
    }

    public WorkoutsEntry()
	{
		InitializeComponent();
	}
}