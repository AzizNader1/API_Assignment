using API_Assignment.DTOs.AttendanceDTOs;

namespace API_Assignment.Services
{
    public interface IAttendanceService
    {
         void AddAttendance(AddAttendanceDto addAttendanceDto);
    }
}
