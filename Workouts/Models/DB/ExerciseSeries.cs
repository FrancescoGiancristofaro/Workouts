﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace WorkoutsApp.Models.DB
{
    public class ExerciseSeries : IAmAModel
    {
        [PrimaryKey,AutoIncrement]
        public int? Id { get; set; }
        public int IdExerciseDetail { get; set; }
        public int IdSeries { get; set; }
    }
}
