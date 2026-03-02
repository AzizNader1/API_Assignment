using System.ComponentModel.DataAnnotations;

namespace API_Assignment.DTOs.ExceptionDTOs
{
    public class AddExceptionDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Can not be greater than 100 character")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public DateTime ExceptionStartDate { get; set; }

        [Required]
        public DateTime ExceptionEndDate { get; set; }
    }
}
