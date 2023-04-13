using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace WorkoutsApp.Models.DB
{
    public class SeriesDetail : IAmAModel
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
        public int Repetitions { get; set; }
        public double Weight { get; set; }
    }
}
