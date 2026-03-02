using API_Assignment.DTOs.ExceptionDtos;
using API_Assignment.DTOs.ExceptionDTOs;
using API_Assignment.UnitOfWork;

namespace API_Assignment.Services
{
    public class ExceptionService : IExceptionService
    {
        private readonly UOW _uow;
        public ExceptionService(UOW uow)
        {
            _uow = uow;
        }

        public void AddException(AddExceptionDto addExceptionDto)
        {
            throw new NotImplementedException();
        }

        public List<ExceptionDto> GetAllExceptions()
        {
            throw new NotImplementedException();
        }

        public List<ExceptionDto> GetExceptionsByUserName(GetExceptionDto getExceptionDto)
        {
            throw new NotImplementedException();
        }
    }
}
