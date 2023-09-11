using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dtos
{
    public partial class SeriesDto : ObservableObject
    {
        public int? Id { get; set; }
        [ObservableProperty] int _repetitions;
        [ObservableProperty] int _secondsRecoveryTime;
        [ObservableProperty] double _weight;
    }

    public partial class SeriesHistoryDto
    {
        public int? Id { get; set; }
        public int Repetitions { get; set; }
        public int SecondsRecoveryTime { get; set; }
        public double Weight { get; set; }
        public int IdWorkoutSession { get; set; }
        public int IdWorkoutExerciseDetails { get; set; }

    }

    public class SeriesHistoryGroupedDto : List<SeriesHistoryDto>
    {
        public int IdWorkoutSession { get; set; }
        public string Date { get; set; }
        public string WorkoutName { get; set; }
        public SeriesHistoryGroupedDto(int idWorkoutSession, DateTime date, string workoutName, List<SeriesHistoryDto> seriesHistoryDtos) : base(seriesHistoryDtos)
        {
            IdWorkoutSession = idWorkoutSession;
            Date = date.ToShortDateString();
            WorkoutName = workoutName;
        }
    }

}
