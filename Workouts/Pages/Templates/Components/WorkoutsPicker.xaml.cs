using CommunityToolkit.Mvvm.Input;

namespace WorkoutsApp.Pages.Templates.Components;

public partial class WorkoutsPicker
{
	public WorkoutsPicker()
	{
		InitializeComponent();
	}

    [RelayCommand]
    void TapFrame()
    {
        Picker.Focus();
    }

    public static readonly BindableProperty TopLabelProperty =
        BindableProperty.Create(
            "TopLabel", typeof(string), typeof(WorkoutsPicker),
            defaultValue: default(string));
    public string TopLabel
    {
        get => (string)GetValue(TopLabelProperty);
        set => SetValue(TopLabelProperty, value);
    }

    public static readonly BindableProperty ItemDisplayBindingProperty =
        BindableProperty.Create(
            "ItemDisplayBinding", typeof(string), typeof(WorkoutsPicker),
            defaultValue: default(string));
    public string ItemDisplayBinding
    {
        get => (string)GetValue(ItemDisplayBindingProperty);
        set => SetValue(ItemDisplayBindingProperty, value);
    }

    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(
            "ItemsSource", typeof(IEnumerable<object>), typeof(WorkoutsPicker),
            defaultValue: default(IEnumerable<object>));
    public IEnumerable<object> ItemsSource
    {
        get => (IEnumerable<object>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public static readonly BindableProperty SelectedItemProperty =
        BindableProperty.Create(
            "SelectedItem", typeof(object), typeof(WorkoutsPicker),
            defaultValue: default(object),BindingMode.TwoWay);
    public object SelectedItem
    {
        get => (object)GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
}