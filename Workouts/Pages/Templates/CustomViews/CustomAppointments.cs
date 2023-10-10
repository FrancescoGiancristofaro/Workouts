using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Maui.Scheduler;

namespace WorkoutsApp.Pages.Templates.CustomViews
{
    public class WorkoutAppointment
    { 
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Subject { get; set; }
        public string RecurrenceRule { get; set; }
        public Brush Background { get; set; }
        public Color TextColor { get; set; }
        public IList<DateTime> RecurrenceExceptionDates { get; set; }
        public int WorkoutTypeId { get; set; } //session or scheduled workout id
    }
    public class WorkoutDoneAppointment : WorkoutAppointment
    {
    }

    public class WorkoutNotDoneAppointment : WorkoutAppointment
    {
    }

    public class WorkoutToDoAppointment : WorkoutAppointment
    {
    }
}
