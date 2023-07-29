using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Repositories.Models;
using Services.Constants;
using Services.Dtos;

namespace Services.Automapper
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            CreateMap<Exercise, ExerciseDto>()
                .ForMember(x=>x.Category,opt=>opt.MapFrom(d=>Enum.Parse(typeof(ExerciseCategory),d.Category)))
                .ForMember(x=>x.Note,x=>x.Ignore())
                .ReverseMap();
        }
    }
}
