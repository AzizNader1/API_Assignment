using API_Assignment.DTOs.ExceptionDtos;
using API_Assignment.DTOs.ExceptionDTOs;
using API_Assignment.UnitOfWork;
using API_Assignment.Models;

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
            if (addExceptionDto == null)
                throw new ArgumentNullException(nameof(addExceptionDto),"the exception data can not left empty");

            var exception = new Models.Exception
            {
                UserName = addExceptionDto.UserName,
                ExceptionStartDate = addExceptionDto.ExceptionStartDate,
                ExceptionEndDate = addExceptionDto.ExceptionEndDate
            };
            _uow.ExceptionRepository.AddAsync(exception);
        }

        public List<ExceptionDto> GetAllExceptions()
        {
            var exceptions = _uow.ExceptionRepository.GetAllAsync().Result;
            if (exceptions == null || !exceptions.Any())
            {
                return new List<ExceptionDto>();
            }
            var exceptionDtos = exceptions.Select(e => new ExceptionDto
            {
                ExceptionId = e.ExceptionId,
                UserName = e.UserName,
                ExceptionStartDate = e.ExceptionStartDate,
                ExceptionEndDate = e.ExceptionEndDate,
                Status = e.Status
            }).ToList();
            return exceptionDtos;
        }

        public List<ExceptionDto> GetExceptionsByUserName(GetExceptionDto getExceptionDto)
        {
            var exceptions = _uow.ExceptionRepository.GetAllAsync().Result;
            if (exceptions == null || !exceptions.Any())
            {
                return new List<ExceptionDto>();
            }
            var filteredExceptions = exceptions.Where(e => e.UserName.Equals(getExceptionDto.UserName, StringComparison.OrdinalIgnoreCase)
                                                 && e.ExceptionStartDate >= getExceptionDto.ExceptionStartDate
                                                 && e.ExceptionEndDate <= getExceptionDto.ExceptionEndDate)
                                                .ToList();
            var exceptionDtos = filteredExceptions.Select(e => new ExceptionDto
            {
                ExceptionId = e.ExceptionId,
                UserName = e.UserName,
                ExceptionStartDate = e.ExceptionStartDate,
                ExceptionEndDate = e.ExceptionEndDate,
                Status = e.Status
            }).ToList();
            return exceptionDtos;
        }
    }
}
