using AutoMapper;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Models;
using GuineaPigApp_Server.Models.Add;
using GuineaPigApp_Server.Models.Get;

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
            CreateMap<Product, GetProductDto>();
            CreateMap<GuineaPigDto, GuineaPig>();
            CreateMap<GuineaPig, GuineaPigDto>();
            CreateMap<GuineaPigWeightDto, GuineaPig>();
            CreateMap<GuineaPigWeight, GuineaPigWeightsDto>();
        }
    }
}
