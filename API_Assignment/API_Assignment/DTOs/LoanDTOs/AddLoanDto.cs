using System.ComponentModel.DataAnnotations;

namespace API_Assignment.DTOs.LoanDTOs
{
    public class AddLoanDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Can not be greater than 100 character")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public double Amount { get; set; }

        [Required]
        public int Installments { get; set; }

        [Required]
        public string Notes { get; set; } = string.Empty;
    }
}
