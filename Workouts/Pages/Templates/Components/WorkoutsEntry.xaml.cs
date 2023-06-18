using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Platform;

namespace WorkoutsApp.Pages.Templates.Components;

public partial class WorkoutsEntry
{
    [RelayCommand]
    void TapFrame()
    {
        Entry.Focus();
    }

    public static readonly BindableProperty MaxLengthProperty =
        BindableProperty.Create(
            "MaxLength", typeof(string), typeof(WorkoutsEntry),
            defaultValue: default(string));
    public string MaxLength
    {
        get => (string)GetValue(MaxLengthProperty);
        set => SetValue(MaxLengthProperty, value);
    }

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

    public static readonly BindableProperty FocusedCommandProperty =
        BindableProperty.Create(
            "FocusedCommand", typeof(ICommand), typeof(WorkoutsEntry),
            defaultValue: default(ICommand));
    public ICommand FocusedCommand
    {
        get => (ICommand)GetValue(FocusedCommandProperty);
        set => SetValue(FocusedCommandProperty, value);
    }

    public static readonly BindableProperty UnfocusedCommandProperty =
        BindableProperty.Create(
            "UnfocusedCommand", typeof(ICommand), typeof(WorkoutsEntry),
            defaultValue: default(ICommand));
    public ICommand UnfocusedCommand
    {
        get => (ICommand)GetValue(UnfocusedCommandProperty);
        set => SetValue(UnfocusedCommandProperty, value);
    }

    public static readonly BindableProperty TextChangedCommandProperty =
        BindableProperty.Create(
            "TextChangedCommand", typeof(ICommand), typeof(WorkoutsEntry),
            defaultValue: default(ICommand));
    public ICommand TextChangedCommand
    {
        get => (ICommand)GetValue(TextChangedCommandProperty);
        set => SetValue(TextChangedCommandProperty, value);
    }
    public WorkoutsEntry()
	{
		InitializeComponent();
	}
}