﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Repositories.Models
{
    /// <summary>
    /// Representation of a series related to a workout's single exercise instance
    /// </summary>
    public class Series : IAmAModel
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
        public int IdWorkoutExerciseDetails { get; set; }
        public int Repetitions { get; set; }
        public int RecoveryTime { get; set; }
        public double Weight { get; set; }
    }
}
