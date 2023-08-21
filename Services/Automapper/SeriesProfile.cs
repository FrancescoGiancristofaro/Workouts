using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Repositories.Models;
using Services.Dtos;

namespace Services.Automapper
{
    public class SeriesProfile : Profile
    {
        public SeriesProfile()
        {
            CreateMap<Series, SeriesDto>()
                .ForMember(x=>x.SecondsRecoveryTime, x=>x.MapFrom(s=>s.RecoveryTime));
            CreateMap<SeriesDto, Series>()
                .ForMember(x=>x.RecoveryTime,x=>x.MapFrom(s=>s.SecondsRecoveryTime))
                .ForMember(x => x.IdWorkoutExerciseDetails, x => x.Ignore())
                .ForMember(x => x.IdWorkout, x => x.Ignore());
        }
    }
}
