using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Dtos;
using WorkoutsApp.Extensions;

namespace WorkoutsApp.Converters
{
    internal class BackgroundRecoveryConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var secondsLeft = values[0] as int?;
            var secondsRecoveryTime = values[1] as int?;

            if (secondsLeft is null || secondsRecoveryTime is null)
                return null;

            var a = (double)secondsLeft / secondsRecoveryTime;

            return a.Value.ToString(CultureInfo.InvariantCulture);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
