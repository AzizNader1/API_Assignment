using API_Assignment.DTOs.LoanDTOs;

namespace API_Assignment.Services
{
    public interface ILoanService
    {
        void AddLoan(AddLoanDto addLoanDto);
        List<LoanDto> GetLoansByUserName(string userName);
    }
}
