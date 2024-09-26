using AutoMapper;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server
{
    public class GuineaPigAppMappingProfile : Profile
    {
        public GuineaPigAppMappingProfile()
        {
            CreateMap<RegisterUserDto, User>();
            CreateMap<GuineaPigDto, GuineaPig>();
            CreateMap<ProductDto, Product>();
        }
    }
}
