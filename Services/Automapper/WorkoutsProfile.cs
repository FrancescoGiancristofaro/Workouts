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

            CreateMap<WorkoutSessionWizardDto, WorkoutSession>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.StartDate, opt => opt.Ignore())
                .ForMember(x => x.EndDate, opt => opt.Ignore());

            CreateMap<WorkoutSession, WorkoutSessionDto>()
                .ForMember(x => x.StartDate, opt => opt.MapFrom(s => DateTimeOffset.FromUnixTimeMilliseconds(s.StartDate).LocalDateTime))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(s => DateTimeOffset.FromUnixTimeMilliseconds(s.EndDate).LocalDateTime));

            CreateMap<WorkoutsScheduled, WorkoutsScheduledDto>()
                .ForMember(x => x.StartDate, opt => opt.MapFrom(s => DateTimeOffset.FromUnixTimeMilliseconds(s.StartDate).LocalDateTime))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(s => s.EndDate == null ? default(DateTime?) : DateTimeOffset.FromUnixTimeMilliseconds(s.EndDate.Value).LocalDateTime));
        }
    }
}
