using API_Assignment.Models;

namespace API_Assignment.DTOs.AttendanceDTOs
{
    public class UpdateAttendanceDto
    {
        public int AttendanceId { get; set; }
        public AttendanceStatus Status { get; set; }
    }
}
