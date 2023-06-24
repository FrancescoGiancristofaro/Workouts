using Repositories.Models;
using Repositories.Repositories;

namespace Services.Services
{
    public interface IExerciseService : IService<Exercise>
    {
    }

    public class ExerciseService : BaseService<Exercise>, IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;

        public ExerciseService(IExerciseRepository exerciseRepository) : base(exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

    }
}
