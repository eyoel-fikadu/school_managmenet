using JetBrains.Annotations;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SMS.SERVICE.DTO.ClassActivityDTO
{
    public class AttendanceModel : AddTableModel
    {
        public int AttendanceId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int ScheduleDetailId { get; set; }
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
        public bool IsActive { get; set; }
    }
    public class AttendanceListModel
    {
        [Required]
        public List<AttendanceModel> attendanceList { get; set; }
    }
}