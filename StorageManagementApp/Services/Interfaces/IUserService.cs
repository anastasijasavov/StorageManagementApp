using Microsoft.AspNetCore.Identity;
using StorageManagementApp.Contracts.DTOs.User;

namespace StorageManagementApp.Mvc.Services.Interfaces
{
    public interface IUserService
    {
        string HashPassword(string password);
        PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword);
        bool CreateUser(UserCreateDto user);
        bool Login(UserLoginDto user);
        bool Delete(int id);
    }
}
