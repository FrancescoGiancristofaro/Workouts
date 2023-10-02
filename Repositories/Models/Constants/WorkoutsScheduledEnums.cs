using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Models.Constants
{
    public enum RecurrenceType
    {
        Daily,
        Weekly,
    }

    public enum RecurrenceEndType
    {
        NoEnd,
        ByEndDate,
        AfterTotOccurence
    }

    public enum DayWeekRecurrence
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
}
