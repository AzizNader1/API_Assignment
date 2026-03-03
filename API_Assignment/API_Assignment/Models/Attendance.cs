using System.ComponentModel.DataAnnotations;

namespace API_Assignment.Models
{
    public enum AttendanceStatus
    {
        [Display(Name = "Approved")] YES,
        [Display(Name = "Not Approved")] NO
    }
    public enum AttendanceTypes
    {
        IN,
        OUT
    }
    public class Attendance
    {
        [Required]
        public int AttendanceId { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public DateTime AttendanceDate { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public AttendanceTypes AttendanceType { get; set; }

        [Required]
        public AttendanceStatus AttendanceStatus { get; set; }

    }
}
