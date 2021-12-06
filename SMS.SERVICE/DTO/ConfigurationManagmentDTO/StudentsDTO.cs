using java.util;
using SMS.SERVICE.DTO.AdmissionDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.DTO.ConfigurationManagmentDTO
{
    public class StudentsModel
    {
        public int calendarYearId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int schoolId { get; set; }
        public String schoolName { get; set; }
        public List<StudentList> studentLists { get; set; }
    }
    public class StudentList
    {
        public int branchId { get; set; }
        public String branchName { get; set; }
        public int classId { get; set; }
        public String className { get; set; }
        public int userId { get; set; }
        public int enrolledStudentId { get; set; }
        public String studentFullName { get; set; }
        public int sectionId { get; set; }
        public String sectionName { get; set; }
    }
}
