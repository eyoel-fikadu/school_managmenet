using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.TeacherPortal.Request
{
    public class GetAttendanceRequest
    {
        [Required]
        [Range(1,int.MaxValue)]
        public int classId { get; set; }
        public int sectionId { get; set; }
        public int subjectId { get; set; }
        public int scheduleId { get; set; }
        public int scheduleDetailId { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
    }
}
