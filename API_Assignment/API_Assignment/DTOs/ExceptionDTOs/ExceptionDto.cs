using API_Assignment.Models;

namespace API_Assignment.DTOs.ExceptionDtos
{
    public class ExceptionDto
    {
        public int ExceptionId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime ExceptionStartDate { get; set; }
        public DateTime ExceptionEndDate { get; set; }
        public ExceptionStatus Status { get; set; } = ExceptionStatus.NO;
    }
}
