using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutsApp.Models.DB;
using WorkoutsApp.Settings;

namespace WorkoutsApp.Repositories
{
    public interface IExerciseRepository : IRepository<Exercise>
    {

    }
    public class ExerciseRepository : BaseRepository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(DBManager dbManager) : base(dbManager)
        {
        }
    }
}
