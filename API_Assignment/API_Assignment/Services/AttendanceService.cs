using API_Assignment.DTOs.AttendanceDTOs;
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
            throw new NotImplementedException();
        }
    }
}
