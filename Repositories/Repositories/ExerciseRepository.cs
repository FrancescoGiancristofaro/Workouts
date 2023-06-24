using Repositories.Models;
using Repositories.Settings;

namespace Repositories.Repositories
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
