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
                throw new ArgumentNullException(nameof(addLoanDto), "the loan data can not left empty");

            var loan = new Loan
            {
                UserName = addLoanDto.UserName,
                Amount = addLoanDto.Amount,
                Installments = addLoanDto.Installments,
                Notes = addLoanDto.Notes
            };
            _uow.LoanRepository.AddEntity(loan);
        }

        public List<LoanDto> GetLoansByUserName(string userName)
        {
            if (String.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName), "User Name wanted to get your loans");

            var loans = _uow.LoanRepository.GetAllEntities();
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

        public List<LoanDto> GetPendingLoans()
        {
            var loans = _uow.LoanRepository.GetAllEntities();
            if (loans == null || !loans.Any())
            {
                return new List<LoanDto>();
            }
            var pendingLoans = loans.Where(e => e.Status == LoanStatus.NO).ToList();
            var loansDto = pendingLoans.Select(e => new LoanDto
            {
                LoanId = e.LoanId,
                UserName = e.UserName,
                Amount = e.Amount,
                Installments = e.Installments,
                Notes = e.Notes,
                Status = e.Status,
            }).ToList();
            return loansDto;
        }

        public void UpdateLoanStatus(int loanId, LoanStatus status)
        {
            if (loanId == 0 || loanId < 0)
                throw new ArgumentNullException(nameof(loanId), "The loan Id can not be 0 or negative");

            if (status == null)
                throw new ArgumentNullException(nameof(status), "The loan Status can not left empty");

            var loan = _uow.LoanRepository.GetEntityById(loanId);
            if (loan == null)
                throw new ArgumentNullException(nameof(loan), $"No loan found with Id: {loanId}");

            loan.Status = status;
            _uow.LoanRepository.UpdateEntity(loan);
        }
    }
}
