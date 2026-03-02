using API_Assignment.DTOs.LoanDTOs;
using API_Assignment.Models;
using API_Assignment.UnitOfWork;

namespace API_Assignment.Services
{
    public class LoanService : ILoanService
    {
        private readonly UOW _uow;
        public LoanService(UOW uow)
        {
            _uow = uow;
        }

        public void AddLoan(AddLoanDto addLoanDto)
        {
            if (addLoanDto == null)
                throw new ArgumentNullException(nameof(addLoanDto),"the loan data can not left empty");

            var loan = new Loan
            {
                UserName = addLoanDto.UserName,
                Amount = addLoanDto.Amount,
                Installments = addLoanDto.Installments,
                Notes = addLoanDto.Notes
            };
            _uow.LoanRepository.AddAsync(loan);
        }

        public List<LoanDto> GetLoansByUserName(string userName)
        {
            if(String.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName),"User Name wanted to get your loans");

            var loans = _uow.LoanRepository.GetAllAsync().Result;
            var userLoans = loans.Where(l => l.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)).ToList();
            var loanDtos = userLoans.Select(l => new LoanDto
            {
                LoanId = l.LoanId,
                UserName = l.UserName,
                Amount = l.Amount,
                Installments = l.Installments,
                Notes = l.Notes,
                Status = l.Status
            }).ToList();
            return loanDtos;
        }
    }
}
