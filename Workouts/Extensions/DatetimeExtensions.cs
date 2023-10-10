using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutsApp.Extensions
{
    public static class DatetimeExtensions
    {
        public static bool IsSameDayThan(this DateTime datetime, DateTime toCompare)
        {
            return datetime.Day.Equals(toCompare.Day) && datetime.Month.Equals(toCompare.Month) && datetime.Year.Equals(toCompare.Year);
        }
    }
}
