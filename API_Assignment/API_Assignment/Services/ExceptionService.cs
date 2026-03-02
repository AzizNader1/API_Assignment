using API_Assignment.DTOs.ExceptionDtos;
using API_Assignment.DTOs.ExceptionDTOs;
using API_Assignment.Models;
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
            if (addExceptionDto == null)
                throw new ArgumentNullException(nameof(addExceptionDto), "the exception data can not left empty");

            var exception = new Models.Exception
            {
                UserName = addExceptionDto.UserName,
                ExceptionStartDate = addExceptionDto.ExceptionStartDate,
                ExceptionEndDate = addExceptionDto.ExceptionEndDate
            };
            _uow.ExceptionRepository.AddEntity(exception);
        }

        public List<ExceptionDto> GetAllExceptions()
        {
            var exceptions = _uow.ExceptionRepository.GetAllEntities();
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
            var exceptions = _uow.ExceptionRepository.GetAllEntities();
            if (exceptions == null || !exceptions.Any())
            {
                return new List<ExceptionDto>();
            }
            var filteredExceptions = exceptions.Where(e => e.UserName.Equals(getExceptionDto.UserName, StringComparison.OrdinalIgnoreCase));

            // now i get the exceptions that match the provided username,
            // but now i will do a check on the date to make sure about two things:
            // 1- each type of the dates are not default value  of the date time (which is 01/01/0001) because we get them throw [FromQuery] properity in the controller
            // then if they are default value that means the user did not provide them
            // and i will not consider them in the filtering process
            // 2- the provided date (if it is not default value) should be between the exception start date and the exception end date

            if (getExceptionDto.ExceptionEndDate != default)
            {
                filteredExceptions = filteredExceptions
                       .Where(e => e.ExceptionEndDate >= getExceptionDto.ExceptionStartDate);
            }
            if (getExceptionDto.ExceptionStartDate != default)
            {
                filteredExceptions = filteredExceptions
                       .Where(e => e.ExceptionStartDate <= getExceptionDto.ExceptionEndDate);
            }
            var result = filteredExceptions.ToList();

            var exceptionDtos = result.Select(e => new ExceptionDto
            {
                ExceptionId = e.ExceptionId,
                UserName = e.UserName,
                ExceptionStartDate = e.ExceptionStartDate,
                ExceptionEndDate = e.ExceptionEndDate,
                Status = e.Status
            }).ToList();
            return exceptionDtos;
        }

        public List<ExceptionDto> GetPendingExceptions()
        {
            var exceptions = _uow.ExceptionRepository.GetAllEntities();
            if (exceptions == null || !exceptions.Any())
            {
                return new List<ExceptionDto>();
            }
            var pendingExceptions = exceptions.Where(e => e.Status == ExceptionStatus.NO).ToList();
            var exceptionDtos = pendingExceptions.Select(e => new ExceptionDto
            {
                ExceptionId = e.ExceptionId,
                UserName = e.UserName,
                ExceptionStartDate = e.ExceptionStartDate,
                ExceptionEndDate = e.ExceptionEndDate,
                Status = e.Status
            }).ToList();
            return exceptionDtos;
        }

        public void UpdateExceptionStatus(int exceptionId, ExceptionStatus status)
        {
            if (exceptionId == 0 || exceptionId < 0)
                throw new ArgumentException("Invalid exception ID. It must be a positive integer.", nameof(exceptionId));

            if (status == null)
                throw new ArgumentNullException(nameof(status), "the new status of the exception can not left empty");

            var exception = _uow.ExceptionRepository.GetEntityById(exceptionId);
            if (exception == null)
                throw new KeyNotFoundException($"No exception found with ID {exceptionId}.");

            exception.Status = status;
            _uow.ExceptionRepository.UpdateEntity(exception);
        }
    }
}
