using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Models.Constants;
using Services.Dtos;

namespace WorkoutsApp.Helpers
{
    public static class SyncfusionSchedulerHelper
    {
        public static string GetRecurrenceRule(WorkoutsScheduledDto dto, bool forcedEndDateNow = false)
        {
            var rule = "FREQ=" + (dto.RecurrenceType is RecurrenceType.Daily ? "DAILY;" : "WEEKLY;");
            rule += dto.Interval > 0 ? $"INTERVAL={dto.Interval};" : "";

            if (!forcedEndDateNow)
            {
                rule += dto.RecurrenceEndType is RecurrenceEndType.AfterTotOccurence && dto.NumOccurencesToEnd > 0 ? $"COUNT={dto.NumOccurencesToEnd};" : "";
                rule += dto.RecurrenceEndType is RecurrenceEndType.ByEndDate && dto.EndDate is not null ? $"UNTIL={dto.EndDate.Value.ToString("yyyyMMddTHHmmssZ")};" : "";
            }
            else
            {
                rule += $"UNTIL={DateTime.UtcNow.AddDays(-1).ToString("yyyyMMddTHHmmssZ")};";

            }
            if (dto.RecurrenceType is RecurrenceType.Weekly)
            {
                var day = MapDayOfWeekToString(dto.DayWeekRecurrence);
                if (!string.IsNullOrWhiteSpace(day))
                    rule += $"BYDAY={day};";
            }

            return rule;
        }

        public static string MapDayOfWeekToString(DayWeekRecurrence recurrence)
        {
            switch (recurrence)
            {
                case DayWeekRecurrence.Monday:
                    return "MO";
                case DayWeekRecurrence.Tuesday:
                    return "TU";
                case DayWeekRecurrence.Wednesday:
                    return "WE";
                case DayWeekRecurrence.Thursday:
                    return "TH";
                case DayWeekRecurrence.Friday:
                    return "FR";
                case DayWeekRecurrence.Saturday:
                    return "SA";
                case DayWeekRecurrence.Sunday:
                    return "SU";
            }

            return null;
        }
    }
}
