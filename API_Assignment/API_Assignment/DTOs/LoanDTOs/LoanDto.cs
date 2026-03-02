using API_Assignment.Models;
using System.ComponentModel.DataAnnotations;

namespace API_Assignment.DTOs.LoanDTOs
{
    public class LoanDto
    {
        public int LoanId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public double Amount { get; set; }

        public int Installments { get; set; }

        public string Notes { get; set; } = string.Empty;

        public LoanStatus Status { get; set; } = LoanStatus.NO;
    }
}
