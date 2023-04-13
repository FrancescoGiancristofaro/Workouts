using SQLite;
using WorkoutsApp.Models.DB;

namespace WorkoutsApp.Settings
{
    public class DBManager
    {
        public SQLiteAsyncConnection Database { get; private set; }

        public DBManager()
        {
        }

        public async Task Init()
        {
            if (Database is not null)
                return;
            if (File.Exists(SqlLiteSettings.DatabasePath))
            {
                Database = new SQLiteAsyncConnection(SqlLiteSettings.DatabasePath, SqlLiteSettings.Flags);
                return;
            }
            Database = new SQLiteAsyncConnection(SqlLiteSettings.DatabasePath, SqlLiteSettings.Flags);
            await Database.CreateTableAsync<Workouts>();
            await Database.CreateTableAsync<WorkoutSession>();
            await Database.CreateTableAsync<WorkoutDetail>();
            await Database.CreateTableAsync<Exercise>();
            await Database.CreateTableAsync<ExerciseDetails>();
            await Database.CreateTableAsync<ExerciseSeries>();
            await Database.CreateTableAsync<SeriesDetail>();
            //await Database.InsertAsync(new Workouts(){Name = "Provatttt"});
        }

    }
}
