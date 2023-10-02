using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Models.Constants;

namespace Services.Dtos
{
    public class WorkoutsScheduledDto
    {
        public int Id { get; set; }
        public int IdWorkout { get; set; }
        public string WorkoutName { get; set; }
        public RecurrenceType RecurrenceType { get; set; }
        public DayWeekRecurrence DayWeekRecurrence { get; set; }
        public RecurrenceEndType RecurrenceEndType { get; set; }
        public byte Interval { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public byte? NumOccurencesToEnd { get; set; }

    }
}
