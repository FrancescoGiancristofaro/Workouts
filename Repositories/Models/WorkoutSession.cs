using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Repositories.Models
{
    /// <summary>
    /// Represents a single instance of a training session that has been executed
    /// </summary>
    public class WorkoutSession : IAmAModel
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
        public int IdWorkout { get; set; }
        public long StartDate { get; set; }
        public long EndDate { get; set; }

    }
}
