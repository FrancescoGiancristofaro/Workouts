using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Exceptions;
using Repositories.Models;
using Repositories.Repositories;
using SQLite;

namespace Services.Services
{
    public interface IMigrationService
    {
        Task Migrate();
    }
    public class MigrationService : IMigrationService
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly ISeriesRepository _seriesRepository;
        private readonly IWorkoutExerciseDetailsRepository _workoutExerciseDetailsRepository;
        private readonly IWorkoutsRepository _workoutsRepository;
        private readonly IMasterRepository _masterRepository;

        #region constructor

        public MigrationService(
            IExerciseRepository exerciseRepository,
            ISeriesRepository seriesRepository,
            IWorkoutExerciseDetailsRepository workoutExerciseDetailsRepository,
            IWorkoutsRepository workoutsRepository,
            IMasterRepository masterRepository)
        {
            _exerciseRepository = exerciseRepository;
            _seriesRepository = seriesRepository;
            _workoutExerciseDetailsRepository = workoutExerciseDetailsRepository;
            _workoutsRepository = workoutsRepository;
            _masterRepository = masterRepository;
        }

        #endregion

        #region public methods

        public async Task Migrate()
        {
            await StartMigrationAsync();
        }

        private async Task DeleteAllDatabaseRecordsAsync()
        {
            await _exerciseRepository.TryDeleteAllAsync();
            await _seriesRepository.TryDeleteAllAsync();
            await _workoutExerciseDetailsRepository.TryDeleteAllAsync();
            await _workoutsRepository.TryDeleteAllAsync();
        }

        #endregion

        #region private methods

        private async Task StartMigrationAsync()
        {
            await _exerciseRepository.Init();
            await _seriesRepository.Init();
            await _workoutExerciseDetailsRepository.Init();
            await _workoutsRepository.Init();

            await VerifyIntegrity<Exercise>();
            await VerifyIntegrity<Series>();
            await VerifyIntegrity<Workouts>();
            await VerifyIntegrity<WorkoutExerciseDetails>();
        }

        private async Task VerifyIntegrity<T>() where T : IAmAModel
        {
            TableMapping? tableMapping = await GetMapping<T>();

            if (tableMapping == null)
                throw new DatabaseIntegrityException();
            var dbColumns = await _masterRepository.GetTableInfo(tableMapping.TableName);

            try
            {
                foreach (var column in dbColumns)
                {
                    var mappingColumn = tableMapping.Columns.FirstOrDefault(x => x.Name == column.Name);
                    if (mappingColumn != null)
                    {
                        var columnType = SQLite.Orm.SqlType(mappingColumn, false, false);
                        if (columnType != column.Type)
                        {
                            if (column.Type == "bigint" && columnType != "datetime")
                            {
                                throw new DatabaseIntegrityException(
                                    $"Column {column.Name} with type {columnType} should be {column.Type}");
                            }
                        }

                        var isUnique = mappingColumn.Indices.Any(s => s.Unique);
                        if (isUnique)
                        {
                            var prop = typeof(T).GetProperties().Single(s => s.Name == column.Name);
                            var attributes = prop.GetCustomAttributes(true);
                            foreach (var attribute in attributes)
                            {
                                var unique = attribute as UniqueAttribute;
                                var indexed = attribute as IndexedAttribute;

                                if (unique is null && indexed is null)
                                {
                                    await RecreateTableWithData<T>();
                                }
                                else if (indexed is not null)
                                {
                                    if (!indexed.Unique)
                                    {
                                        await RecreateTableWithData<T>();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        await RecreateTableWithData<T>();
                    }
                }
            }
            catch (DatabaseIntegrityException ex)
            {
                await DeleteAndInitTable<T>();
            }
        }

        private async Task DeleteAndInitTable<T>()
        {
            if (typeof(T) == typeof(Exercise))
            {
                await _exerciseRepository.DeleteTable();
                await _exerciseRepository.Init();
            }

            if (typeof(T) == typeof(Series))
            {
                await _seriesRepository.DeleteTable();
                await _seriesRepository.Init();
            }
            if (typeof(T) == typeof(Workouts))
            {
                await _workoutsRepository.DeleteTable();
                await _workoutsRepository.Init();
            }
            if (typeof(T) == typeof(WorkoutExerciseDetails))
            {
                await _workoutExerciseDetailsRepository.DeleteTable();
                await _workoutExerciseDetailsRepository.Init();
            }
        }

        private async Task RecreateTableWithData<T>()
        {
            if (typeof(T) == typeof(Exercise))
            {
                await _exerciseRepository.RecreateTableWithData();
            }
            if (typeof(T) == typeof(Series))
            {
                await _seriesRepository.RecreateTableWithData();
            }
            if (typeof(T) == typeof(Workouts))
            {
                await _workoutsRepository.RecreateTableWithData();
            }
            if (typeof(T) == typeof(WorkoutExerciseDetails))
            {
                await _workoutExerciseDetailsRepository.RecreateTableWithData();
            }

        }

        private async Task<TableMapping?> GetMapping<T>()
        {
            TableMapping? tableMapping = null;
            if (typeof(T) == typeof(Exercise))
            {
                tableMapping = await _exerciseRepository.GetMapping();
            }
            if (typeof(T) == typeof(Series))
            {
                tableMapping = await _seriesRepository.GetMapping();
            }
            if (typeof(T) == typeof(Workouts))
            {
                tableMapping = await _workoutsRepository.GetMapping();
            }
            if (typeof(T) == typeof(WorkoutExerciseDetails))
            {
                tableMapping = await _workoutExerciseDetailsRepository.GetMapping();
            }

            return tableMapping;
        }

        #endregion
    }
}
