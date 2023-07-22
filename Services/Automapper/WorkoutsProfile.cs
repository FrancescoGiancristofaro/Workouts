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
    public class WorkoutsProfile : Profile
    {
        public WorkoutsProfile()
        {
            CreateMap<Workouts, WorkoutsDto>();
        }
    }
}
