using API_Assignment.DTOs.AttendanceDTOs;
using API_Assignment.Models;
using API_Assignment.UnitOfWork;

namespace API_Assignment.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly UOW _uow;
        public AttendanceService(UOW uow)
        {
            _uow = uow;
        }

        public void AddAttendance(AddAttendanceDto addAttendanceDto)
        {
            if (addAttendanceDto == null)
                throw new ArgumentNullException(nameof(addAttendanceDto), "The Attendance data can not left empty");

            var attendance = new Attendance
            {
                UserName = addAttendanceDto.UserName,
                AttendanceDate = addAttendanceDto.AttendanceDate,
                Longitude = addAttendanceDto.Longitude,
                Latitude = addAttendanceDto.Latitude,
                AttendanceType = addAttendanceDto.AttendanceType,
                AttendanceStatus = AttendanceStatus.NO
            };
            _uow.AttendanceRepository.AddEntity(attendance);
        }

        public List<AttendanceDto> GetPendingAttendances()
        {
            var allAttendances = _uow.AttendanceRepository.GetAllEntities();

            if (allAttendances == null || !allAttendances.Any())
                throw new ArgumentNullException("No attendances found.");

            return allAttendances.Where(att => att.AttendanceStatus == AttendanceStatus.NO)
                    .Select(att => new AttendanceDto
                    {
                        AttendanceId = att.AttendanceId,
                        UserName = att.UserName,
                        AttendanceDate = att.AttendanceDate,
                        Longitude = att.Longitude,
                        Latitude = att.Latitude,
                        AttendanceType = att.AttendanceType,
                        AttendanceStatus = att.AttendanceStatus
                    }).ToList();
        }

        public void UpdateAttendanceStatus(int attendanceId, AttendanceStatus attendanceStatus)
        {
            if (attendanceId == 0 || attendanceId < 0)
                throw new ArgumentNullException(nameof(attendanceId), "The Attendance Id can not be 0 or negative");

            if (attendanceStatus == null)
                throw new ArgumentNullException(nameof(attendanceStatus), "The Attendance Status can not left empty");

            var attendance = _uow.AttendanceRepository.GetEntityById(attendanceId);
            if (attendance == null)
                throw new ArgumentNullException(nameof(attendance), $"No attendance found with Id: {attendanceId}");

            attendance.AttendanceStatus = attendanceStatus;
            _uow.AttendanceRepository.UpdateEntity(attendance);

        }
    }
}
