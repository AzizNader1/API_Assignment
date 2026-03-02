using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace API_Assignment.Models
{
    public enum Roles
    {
        Employee,
        Adming
    }
    public class User
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
