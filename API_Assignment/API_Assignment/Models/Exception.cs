using System.ComponentModel.DataAnnotations;

namespace API_Assignment.Models
{
    public enum ExceptionStatus
    {
        [Display(Name = "Approved")] YES,
        [Display(Name = "Not Approved")] NO
    }
    public class Exception
    {
        [Required]
        public int ExceptionId { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public DateTime ExceptionStartDate { get; set; }

        [Required]
        public DateTime ExceptionEndDate { get; set; }

        [Required]
        public ExceptionStatus Status { get; set; } = ExceptionStatus.NO;
    }
}
