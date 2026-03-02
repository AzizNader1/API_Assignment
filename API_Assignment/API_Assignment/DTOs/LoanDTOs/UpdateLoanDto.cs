using API_Assignment.Models;

namespace API_Assignment.DTOs.LoanDTOs
{
    public class UpdateLoanDto
    {
        public int LoanId { get; set; }
        public LoanStatus Status { get; set; }
    }
}
