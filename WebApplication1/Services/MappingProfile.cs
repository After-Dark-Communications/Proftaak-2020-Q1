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
        }
    }
}
