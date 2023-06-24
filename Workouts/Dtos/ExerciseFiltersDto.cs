using Repositories.Constants;
namespace WorkoutsApp.Dtos
{
    public class ExerciseFiltersDto
    {
        public ExerciseCategory Category { get; set; }
        public string TextToSearch { get; set; }
    }
}
