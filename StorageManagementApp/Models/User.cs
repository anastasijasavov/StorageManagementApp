using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace StorageManagementApp.Models
{
    public class User 
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
    }
}
