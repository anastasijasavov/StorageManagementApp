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
        public UserService(StorageDBContext ctx, IMapper mapper, ILogger<UserService> logger)
        {
            _ctx = ctx;
            _mapper = mapper;
            _logger = logger;
        }

        public bool CreateUser(UserCreateDto user)
        {
            try
            {
                User dbUser = _mapper.Map<User>(user);

                //generate password hash
                dbUser.PasswordHash = HashPassword(user.Password);
                //add normalized username
                dbUser.NormalizedUserName = user.UserName.ToUpper();

                _ctx.Users.Add(dbUser);
                _ctx.SaveChanges();
                _logger.LogInformation("Created user " + user.UserName);

                return true;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
                return false;
                throw;
            }

        }

        public bool Delete(int id)
        {
            try
            {
                var user = _ctx.Users.Where(x => x.Id == id).FirstOrDefault();
                if (user != null)
                {
                    _ctx.Users.Remove(user);
                    _ctx.SaveChanges();
                    _logger.LogInformation("Deleted user " + user.UserName);
                    return true;

                }
                else
                {
                    _logger.LogInformation("User not found for deletion.");
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
                return false;
                throw;
            }
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 12);
        }

        public bool Login(UserLoginDto user)
        {
            var dbuser = _ctx.Users.Where(x => x.UserName.Equals(user.UserName)).FirstOrDefault();
            PasswordVerificationResult result = PasswordVerificationResult.Failed;

            if (dbuser != null)
                result = VerifyHashedPassword(dbuser.PasswordHash, user.Password);

            if (result == PasswordVerificationResult.Success)
            {
                _logger.LogInformation("Login successful for user " + dbuser?.UserName);
                return true;
            }
            else if (result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                try
                {
                    dbuser.PasswordHash = HashPassword(dbuser.Password);
                    _logger.LogInformation("Rehashing password for user " + dbuser.UserName);

                    _ctx.Update(dbuser);
                    _ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    _logger.Log(LogLevel.Error, e.Message);
                    return false;
                    throw;
                }
            }
            return false;

        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            var isValid = BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);

            if (isValid && BCrypt.Net.BCrypt.PasswordNeedsRehash(hashedPassword, 12))
            {
                return PasswordVerificationResult.SuccessRehashNeeded;
            }
            return isValid ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }
    }
}
