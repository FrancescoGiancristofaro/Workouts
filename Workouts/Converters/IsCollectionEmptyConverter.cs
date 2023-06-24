using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutsApp.Extensions;

namespace WorkoutsApp.Converters
{
    internal class IsCollectionEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IList<dynamic> list)
            {
                return list.SafeAny();
            }
            if (value is IEnumerable<dynamic> iEnumerable)
            {
                return iEnumerable.SafeAny();
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
