using Microsoft.AspNetCore.Identity;
using StorageManagementApp.Contracts.DTOs.User;

namespace StorageManagementApp.Mvc.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUser(UserCreateDto user);
        Task<bool> Login(UserLoginDto user);
        Task<bool> Delete(string email);
        Task<bool> Logout();
        Task<bool> UpdateUser(UserCreateDto dto);
    }
}
