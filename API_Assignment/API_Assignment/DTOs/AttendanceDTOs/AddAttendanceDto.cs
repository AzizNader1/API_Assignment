using API_Assignment.Models;
using System.ComponentModel.DataAnnotations;

namespace API_Assignment.DTOs.AttendanceDTOs
{
    public class AddAttendanceDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Can not be greater than 100 character")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public DateTime AttendanceDate { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public double Longitude { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public double Latitude { get; set; }

        [Required]
        public AttendanceTypes AttendanceType { get; set; }

    }
}
