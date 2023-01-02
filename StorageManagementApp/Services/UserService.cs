using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StorageManagementApp.Contracts.DTOs.User;
using StorageManagementApp.Models;
using StorageManagementApp.Mvc.Services.Interfaces;

namespace StorageManagementApp.Mvc.Services
{
    public class UserService : IUserService
    {
        private ILogger<UserService> _logger;
        private readonly StorageDBContext _ctx;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(
            StorageDBContext ctx,
            IMapper mapper, 
            ILogger<UserService> logger,
            UserManager<User> userManager, 
            SignInManager<User> signInManager
        )
        {
            _ctx = ctx;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> CreateUser(UserCreateDto user)
        {

            var result = await _userManager.CreateAsync(new User 
            { 
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
            }, user.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("Created user " + user.UserName);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Delete(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                _logger.LogInformation("Deleted user " + user.UserName);
                return true;

            }
            else
            {
                _logger.LogInformation("User not found for deletion.");
                return false;
            }

        }

        public async Task<bool> Login(UserLoginDto user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName,
                         user.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Logout()
        {
            await this._signInManager.SignOutAsync();
            return true;
        }

        public Task<bool> UpdateUser(UserCreateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
