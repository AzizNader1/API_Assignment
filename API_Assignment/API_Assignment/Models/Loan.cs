using System.ComponentModel.DataAnnotations;

namespace API_Assignment.Models
{
    public enum LoanStatus
    {
       YES,
       NO
    }
    public class Loan
    {
        [Required]
        public int LoanId { get; set; }

        [Required]
         
        public string UserName { get; set; } = string.Empty;

        [Required]
        public double Amount { get; set; }

        [Required]
        public int Installments { get; set; }

        [Required]
        public string Notes { get; set; } = string.Empty;

        [Required]
        public LoanStatus Status { get; set; } = LoanStatus.NO;

    }
}
