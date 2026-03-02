using API_Assignment.Models;
using System.ComponentModel.DataAnnotations;

namespace API_Assignment.Services.UserDTOs
{
    public class GetUserDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public Roles UserRole { get; set; }
    }
}
