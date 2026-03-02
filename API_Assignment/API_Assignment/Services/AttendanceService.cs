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
                throw new ArgumentNullException(nameof(addAttendanceDto),"The Attendance data can not left empty");

            var attendance = new Attendance
            {
                UserName = addAttendanceDto.UserName,
                AttendanceDate = addAttendanceDto.AttendanceDate,
                Longitude = addAttendanceDto.Longitude,
                Latitude = addAttendanceDto.Latitude,
                AttendanceType = addAttendanceDto.AttendanceType
            };
            _uow.AttendanceRepository.AddAsync(attendance);
        }
    }
}
