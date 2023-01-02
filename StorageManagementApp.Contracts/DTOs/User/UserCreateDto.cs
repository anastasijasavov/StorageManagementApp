using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagementApp.Contracts.DTOs.User
{
    public class UserCreateDto
    {
        [Required]
        [StringLength(15, MinimumLength = 5,
      ErrorMessage = "Username should be minimum 5 characters and a maximum of 15 characters")]
        [RegularExpression(@"^[a-zA-Z]+(_)?[a-zA-Z]{0,}$", ErrorMessage = "Invalid symbols.")]
        public string UserName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6,
      ErrorMessage = "Password should be minimum 6 characters and a maximum of 20 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z]).{6,20}$", ErrorMessage = "Password should contain at least 1 uppercase, 1 lowercase letter, and a special symbol (@, -, | or _).")]
        public string Password { get; set; }
        [StringLength(20, MinimumLength = 6,
      ErrorMessage = "Password should be minimum 6 characters and a maximum of 20 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z]).{6,20}$", ErrorMessage = "Password should contain at least 1 uppercase, 1 lowercase letter, and a special symbol (@, -, | or _).")]
        [Compare("Password")]
        public string RePassword { get; set; }
        [Required]
        public string Email { get; set; }
        [RegularExpression(@"^([0-9]{1,})(([\s.-]?)(\d{1,})){1,}$", ErrorMessage = "Invalid number format. Allowed characters: 0-9, - and space")]
        public string? PhoneNumber { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
