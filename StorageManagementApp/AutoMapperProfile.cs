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
            CreateMap<User, UserCreateDto>();
            CreateMap<Product, ProductCreateDto>();

        }
    }
}
