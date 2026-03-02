using System.ComponentModel.DataAnnotations;

namespace API_Assignment.DTOs.ExceptionDTOs
{
    public class AddExceptionDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        public DateTime ExceptionStartDate { get; set; }
        public DateTime ExceptionEndDate { get; set; }
    }
}
