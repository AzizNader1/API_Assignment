using API_Assignment.DTOs.AttendanceDTOs;
using API_Assignment.Models;

namespace API_Assignment.Services
{
    public interface IAttendanceService
    {
        void AddAttendance(AddAttendanceDto addAttendanceDto);
        List<AttendanceDto> GetPendingAttendances();
        void UpdateAttendanceStatus(int attendanceId, AttendanceStatus attendanceStatus);
    }
}