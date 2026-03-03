using System.ComponentModel.DataAnnotations;

namespace API_Assignment.DTOs.UserDTOs
{
    public class AddToRoleDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
