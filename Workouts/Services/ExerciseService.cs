using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutsApp.Models.DB;
using WorkoutsApp.Repositories;

namespace WorkoutsApp.Services
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
