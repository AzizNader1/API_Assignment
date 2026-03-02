using API_Assignment.Models;

namespace API_Assignment.DTOs.ExceptionDTOs
{
    public class UpdateExceptionDto
    {
        public int ExceptionId { get; set; }
        public ExceptionStatus Status { get; set; }
    }
}
