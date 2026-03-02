using System.ComponentModel.DataAnnotations;

namespace API_Assignment.DTOs.UserDTOs
{
    public class LoginUserDto
    {
        [Required]
        [MaxLength(100,ErrorMessage ="Can not be greater than 100 character")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100,ErrorMessage = "The password Length must not be greater than 100 character")]
        [MinLength(8,ErrorMessage ="The password Length can not be less than 8 characters")]
        public string Password { get; set; } = string.Empty;
    }
}
