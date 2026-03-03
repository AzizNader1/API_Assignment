using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace API_Assignment.Models
{
    public class User : IdentityUser
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

    }
}
