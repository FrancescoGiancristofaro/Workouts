using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Constants;
using SQLite;

namespace Repositories.Models
{
    public class Exercise : IAmAModel
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
        public string Name { get; set; }
        public ExerciseCategory Category { get; set; }
        public string Description { get; set; }
    }
}
