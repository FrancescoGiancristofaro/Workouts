using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Constants;

namespace Services.Dtos
{
    public class ExerciseDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public ExerciseCategory Category { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }

    }
}
