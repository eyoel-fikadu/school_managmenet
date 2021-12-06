using java.sql;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SMS.SERVICE.DTO.ClassActivityDTO
{
    public class TimeTableModel : AddTableModel
    {
        public int TimeTableId { get; set; }
        [Required]
        public String Description { get; set; }
        public int BatchId { get; set; }
        public int BranchId { get; set; }
        [Required]
        public String DayOfTheWeek { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public String StartTimeResult { get; set; }
        public String EndTimeResult { get; set; }
        public bool IsActive { get; set; }

    }
}
