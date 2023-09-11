using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutsApp.Converters
{
    public class SecondsRecoveryToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int minutes = (int)value / 60;
            int seconds = (int)value % 60;
            string secondsString = seconds < 10 ? "0" + seconds : $"{seconds}";
            return $"{minutes}:{secondsString}";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var values = value.ToString().Split(':');
            var minutesInSeconds = Int32.Parse(values[0])*60;
            var seconds = Int32.Parse(values[1]);
            return minutesInSeconds + seconds;
        }
    }
}
