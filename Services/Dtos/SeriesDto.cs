using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dtos
{
    public class SeriesDto
    {
        public int? Id { get; set; }
        public int Repetitions { get; set; }
        public int RecoveryTime { get; set; }
        public double Weight { get; set; }
    }
}
