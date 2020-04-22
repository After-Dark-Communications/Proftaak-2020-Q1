using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using DAL.Models;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class MappingProfile : Profile
    { 
        public MappingProfile()
        {
            CreateMap<TramDTO, Tram>().ReverseMap();
            CreateMap<TramDTO, TramViewModel>().ReverseMap();
            CreateMap<StatusDTO, Status>().ReverseMap();
            CreateMap<StatusDTO, StatusViewModel>().ReverseMap();
            CreateMap<TrackDTO, Track>().ReverseMap();
            CreateMap<TrackDTO, TrackViewModel>().ReverseMap();
            CreateMap<SectorDTO, Sector>().ReverseMap();
            CreateMap<SectorDTO, SectorViewModel>().ReverseMap();

        }
    }
}
