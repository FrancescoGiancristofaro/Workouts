﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dtos
{
    public class ExerciseSeriesDto
    {
        public int IdWorkoutExercise { get; set; }
        public List<SeriesDto> Series { get; set; }
    }
}
