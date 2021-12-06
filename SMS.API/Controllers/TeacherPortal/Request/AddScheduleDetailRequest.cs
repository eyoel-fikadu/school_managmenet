using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.TeacherPortal.Request
{
    public class Teacher_AddScheduleDetailRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ScheduleId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
