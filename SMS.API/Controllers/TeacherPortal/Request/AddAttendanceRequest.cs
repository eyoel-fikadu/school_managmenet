using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.TeacherPortal.Request
{
    public class AddAttendanceRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ScheduleDetailId { get; set; }
        [Required]
        [NotNull]

        public List<AttendanceRequest> attendances { get; set; }
    }
    public class AttendanceRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int StudentId { get; set; }
        [Required]
        [NotNull]
        public bool Present { get; set; }
        [Required]
        [NotNull]
        public bool Permission { get; set; }
        [Required]
        [NotNull]
        public bool Late { get; set; }
    }
}
