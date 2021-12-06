using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal.Request
{
    public class AddScheduleRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int TimeTableId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int SubjectId { get; set; }
    }
    public class AddScheduleDetailRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ScheduleId { get; set; }
        public int TeacherId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
