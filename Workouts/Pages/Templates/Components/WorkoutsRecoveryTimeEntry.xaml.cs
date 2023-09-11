using System.Text.RegularExpressions;
using WorkoutsApp.Extensions;

namespace WorkoutsApp.Pages.Templates.Components
{
    public partial class WorkoutsRecoveryTimeEntry
    {
        public WorkoutsRecoveryTimeEntry()
        {
            InitializeComponent();
        }


        public static readonly BindableProperty TextProperty =
        BindableProperty.Create(
            "Text", typeof(string), typeof(WorkoutsRecoveryTimeEntry),
            defaultValue: default(string), BindingMode.OneWay);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);

        }

        public static readonly BindableProperty SecondsProperty =
        BindableProperty.Create(
            "Seconds", typeof(int), typeof(WorkoutsRecoveryTimeEntry),
            defaultValue: default(int), BindingMode.TwoWay);

        public int Seconds
        {
            get => (int)GetValue(SecondsProperty);
            set => SetValue(SecondsProperty, value);
            
        }

        public static readonly BindableProperty SecondsStartProperty =
        BindableProperty.Create(
            "SecondsStart", typeof(int), typeof(WorkoutsRecoveryTimeEntry),
            defaultValue: default(int), BindingMode.OneWay);

        public int SecondsStart
        {
            get => (int)GetValue(SecondsStartProperty);
            set => SetValue(SecondsStartProperty, value);

        }

        public static readonly BindableProperty FontSizeProperty =
        BindableProperty.Create(
            "FontSize", typeof(string), typeof(WorkoutsRecoveryTimeEntry),
            defaultValue: default(string), BindingMode.TwoWay);

        public string FontSize
        {
            get => (string)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        private bool _notExecuteTextChanged = false;

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(Text) || Text.Contains(':'))
                return;

            _notExecuteTextChanged = true;
            var secondsChoiced = Int32.Parse(Text.Replace(":", ""));
            int minutes = secondsChoiced / 60;
            minutes = minutes > 99 ? 99 : minutes;
            int seconds = secondsChoiced % 60;
            string secondsString = seconds < 10 ? "0" + seconds : $"{seconds}";
            Text = $"{minutes}:{secondsString}";
            SetValue(SecondsProperty, minutes + seconds);
        }

        private int GetSecondsFromText()
        {
            Text = Text.Trim();
            var splitted = Text.Split(':');
            if(string.IsNullOrEmpty(splitted[0]))
                return (int)GetValue(SecondsStartProperty);

            if(splitted.Length is 1 || string.IsNullOrEmpty(splitted[1]))
            {
                return Int32.Parse(Regex.Replace(splitted[0], "[^0-9]", ""));
            }
            int minutes = Int32.Parse(Regex.Replace(splitted[0], "[^0-9]", "")) * 60;
            var seconds = Int32.Parse(Regex.Replace(splitted[1], "[^0-9]", ""));
            var minutesOfSeconds = seconds / 60;
            minutes += minutesOfSeconds;
            seconds = seconds % 60;
            return minutes + seconds;
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (_notExecuteTextChanged)
                {
                    _notExecuteTextChanged = false;
                    return;
                }

                var args = e as TextChangedEventArgs;
                if (args.NewTextValue.SafeAny(x => x is not ':' && !Char.IsDigit(x)))
                {
                    Text = "";
                    _notExecuteTextChanged = true;
                    return;
                }

                if (args.NewTextValue?.Length > args.OldTextValue?.Length)
                {
                    if (args.NewTextValue?.Length == 3 && !args.NewTextValue.SafeAny(x => x is ':'))
                    {
                        Text = args.NewTextValue.Insert(1, ":");
                        _notExecuteTextChanged = true;
                        return;
                    }

                    if (args.NewTextValue?.Length == 5)
                    {
                        var index = args.NewTextValue.IndexOf(':');
                        var a = args.NewTextValue.Remove(index, 1);
                        a = a.Insert(2, ":");
                        Text = a;
                        _notExecuteTextChanged = true;
                        return;
                    }

                    if (args.NewTextValue?.Length < 3)
                    {
                        var index = args.NewTextValue.IndexOf(':');
                        if (index is -1)
                            return;

                        args.NewTextValue.Remove(index);
                        Text = args.NewTextValue;
                        _notExecuteTextChanged = true;
                        return;
                    }
                }
                else
                {
                    if (args.NewTextValue?.Length == 4)
                    {
                        var index = args.NewTextValue.IndexOf(':');
                        var a = args.NewTextValue.Remove(index, 1);
                        a = a.Insert(1, ":");
                        Text = a;
                        _notExecuteTextChanged = true;
                        return;
                    }

                    if (args.NewTextValue?.Length == 3)
                    {
                        var index = args.NewTextValue.IndexOf(':');
                        Text = args.NewTextValue.Remove(index, 1);
                        _notExecuteTextChanged = true;
                        return;
                    }
                }
            }
            finally
            {
                SetValue(SecondsProperty, GetSecondsFromText());
            }
        }
    }
}