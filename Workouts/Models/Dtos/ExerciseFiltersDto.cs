using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutsApp.Constants;

namespace WorkoutsApp.Models.Dtos
{
    public class ExerciseFiltersDto
    {
        public ExerciseCategory Category { get; set; }
        public string TextToSearch { get; set; }
    }
}
