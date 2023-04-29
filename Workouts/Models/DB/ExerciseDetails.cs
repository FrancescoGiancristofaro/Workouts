﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace WorkoutsApp.Models.DB
{
    public class ExerciseDetails : IAmAModel
    {
        [PrimaryKey,AutoIncrement]
        public int? Id { get; set; }
        public int IdExercise { get; set; }
        public string Note { get; set; }
    }
}