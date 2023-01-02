using AutoMapper;
using StorageManagementApp.Contracts.DTOs.Product;
using StorageManagementApp.Contracts.DTOs.User;
using StorageManagementApp.Models;

namespace StorageManagementApp.Mvc
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserCreateDto, User>();

            CreateMap<ProductDto, Product>()
            .ForMember(x => x.CategoryId, opt => opt.MapFrom(y => (int)y.CategoryId));

            CreateMap<Product, ProductDto>();

            CreateMap<Product, ProductViewDto>()
                .ForMember(x => x.CategoryName,
                           y => y.MapFrom(x => x.Category.Name));


        }
    }
}
