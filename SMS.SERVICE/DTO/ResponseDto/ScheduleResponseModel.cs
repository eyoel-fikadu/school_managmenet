using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.DTO.ResponseDto
{
    public class ScheduleResponseModel
    {
        public int scheduleId { get; set; }
        public int timeTableId { get; set; }
        public string timeTableDescription { get; set; }
        public string dayOfWeek { get; set; }
        public int classId { get; set; }
        public string className { get; set; }
        public int sectionId { get; set; }
        public string sectionName { get; set; }
        public int subjectId { get; set; }
        public string subjectName { get; set; }
        public bool isActive { get; set; }
        public int employeeId { get; set; }
        public String teacherName { get; set; }
    }
}
