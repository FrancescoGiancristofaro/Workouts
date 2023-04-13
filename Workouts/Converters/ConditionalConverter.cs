using System.Globalization;

namespace WorkoutsApp.Converters
{
    public class ConditionalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var conditionalParameter = parameter as ConditionalConverterParameterValues;
            var booleanValue = (bool)value;
            return booleanValue ? conditionalParameter.TrueValue : conditionalParameter.FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConditionalConverterParameter : IMarkupExtension
    {
        public object TrueValue { get; set; }
        public object FalseValue { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return new ConditionalConverterParameterValues() { TrueValue = (object)TrueValue, FalseValue = (object)FalseValue };
        }
    }
    public class ConditionalConverterParameterValues
    {
        public object TrueValue { get; set; }
        public object FalseValue { get; set; }
    }
}