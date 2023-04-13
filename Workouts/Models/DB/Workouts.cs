using SQLite;

namespace WorkoutsApp.Models.DB
{
    public class Workouts : IAmAModel
    {
        [PrimaryKey,AutoIncrement]
        public int? Id { get; set; }

        public string Name { get; set; }
    }
}
