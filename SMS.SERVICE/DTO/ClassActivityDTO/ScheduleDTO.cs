using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SMS.SERVICE.DTO.ClassActivityDTO
{
    public class ScheduleModel : AddTableModel
    {
        public int ScheduleId { get; set; }
        [Required]
        [Range(1,int.MaxValue)]
        public int TimeTableId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int SubjectId { get; set; }
        public bool IsActive { get; set; }
    }
    public class ScheduleDetailModel : AddTableModel
    {
        public int ScheduleDetailId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int ScheduleId { get; set; }
        public int TeacherId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }

}
