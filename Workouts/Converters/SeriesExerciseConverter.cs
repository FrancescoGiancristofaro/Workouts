using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutsApp.Models.Dtos;

namespace WorkoutsApp.Converters
{
    public class SeriesExerciseConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            if (values.All(x => x is not null))
            {
                var se = values[0] as SeriesDto;
                var ex = values[1] as SelectableExerciseDto;
                return Tuple.Create(se, ex);
            }
            return new object();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
