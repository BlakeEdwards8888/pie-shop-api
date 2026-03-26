using AutoMapper;
using PieShop.API.Entities;
using PieShop.API.Models;

namespace PieShop.API.Profiles
{
    public class PieProfile : Profile
    {
        public PieProfile()
        {
            CreateMap<Pie, PieDto>();
            CreateMap<PieCreationDto, Pie>();
            CreateMap<PieUpdateDto, Pie>();
        }
    }
}
