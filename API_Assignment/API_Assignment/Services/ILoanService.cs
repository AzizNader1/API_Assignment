using API_Assignment.DTOs.LoanDTOs;
using API_Assignment.Models;

namespace API_Assignment.Services
{
    public interface ILoanService
    {
        void AddLoan(AddLoanDto addLoanDto);
        List<LoanDto> GetLoansByUserName(string userName);
        List<LoanDto> GetPendingLoans();
        void UpdateLoanStatus(int loanId, LoanStatus status);
    }
}
