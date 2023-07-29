using Repositories.Models;
using SQLite;

namespace Repositories.Settings
{
    public static class SqlLiteSettings
    {
        public const string DatabaseFilename = "TodoSQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;
        
    }

    public class DBManager
    {
        private readonly string _databasePath;
        public SQLiteAsyncConnection? Database { get; private set; }

        public DBManager(string databasePath)
        {
            _databasePath = Path.Combine(databasePath, SqlLiteSettings.DatabaseFilename);
        }

        public async Task Init()
        {
            if (Database is not null)
                return;
            if (File.Exists(_databasePath))
            {
                Database = new SQLiteAsyncConnection(_databasePath, SqlLiteSettings.Flags);
                return;
            }
            Database = new SQLiteAsyncConnection(_databasePath, SqlLiteSettings.Flags);
            await Database.CreateTableAsync<Workouts>();
            await Database.CreateTableAsync<WorkoutSession>();
            await Database.CreateTableAsync<Exercise>();
            await Database.CreateTableAsync<WorkoutExerciseDetails>();
            await Database.CreateTableAsync<Series>();
        }

    }
}
