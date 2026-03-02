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
