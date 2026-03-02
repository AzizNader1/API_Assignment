using API_Assignment.DTOs.AttendanceDTOs;
using API_Assignment.DTOs.ExceptionDtos;
using API_Assignment.DTOs.ExceptionDTOs;
using API_Assignment.Models;

namespace API_Assignment.Services
{
    public interface IExceptionService
    {
        void AddException(AddExceptionDto addExceptionDto);
        List<ExceptionDto> GetAllExceptions();
        List<ExceptionDto> GetExceptionsByUserName(GetExceptionDto getExceptionDto);
        List<ExceptionDto> GetPendingExceptions();
        void UpdateExceptionStatus(int exceptionId, ExceptionStatus status);
    }
}
