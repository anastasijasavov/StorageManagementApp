using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace StorageManagementApp.Models
{
    public class User : IdentityUser
    {
        public string? PhoneNumber { get; set; }
    }
}
