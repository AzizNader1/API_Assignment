using API_Assignment.Models;
using System.ComponentModel.DataAnnotations;

namespace API_Assignment.DTOs.AttendanceDTOs
{
    public class AttendanceDto
    {
        public int AttendanceId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public DateTime AttendanceDate { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public AttendanceTypes AttendanceType { get; set; }

        public AttendanceStatus AttendanceStatus { get; set; } = AttendanceStatus.NO;
    }
}
