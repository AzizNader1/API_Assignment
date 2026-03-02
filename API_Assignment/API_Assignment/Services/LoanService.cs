using API_Assignment.DTOs.LoanDTOs;
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
            throw new NotImplementedException();
        }
    }
}
