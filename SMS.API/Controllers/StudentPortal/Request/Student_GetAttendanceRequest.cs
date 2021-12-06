using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.StudentPortal.Request
{
    public class Student_GetAttendanceRequest
    {
        public int subjectId { get; set; }
        public int scheduleId { get; set; }
        public int scheduleDetailId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
