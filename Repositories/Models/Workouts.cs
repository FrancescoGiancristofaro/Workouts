using SQLite;

namespace Repositories.Models
{
    public class Workouts : IAmAModel
    {
        /// <summary>
        /// Representation of a workout
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
