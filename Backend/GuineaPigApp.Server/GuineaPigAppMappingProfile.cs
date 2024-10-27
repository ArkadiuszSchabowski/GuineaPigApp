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
            CreateMap<UpdateUserDto, User>();
            CreateMap<User, GetUserDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
            CreateMap<GuineaPigDto, GuineaPig>();
            CreateMap<GuineaPig, GuineaPigDto>();
            CreateMap<GuineaPigWeightDto, GuineaPig>();
            CreateMap<GuineaPigWeightDto, GuineaPigWeight>();
        }
    }
}
