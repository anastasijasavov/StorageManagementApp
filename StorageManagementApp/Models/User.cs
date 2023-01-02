using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace StorageManagementApp.Models
{
    public class User : IdentityUser
    {
        public int Id { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
