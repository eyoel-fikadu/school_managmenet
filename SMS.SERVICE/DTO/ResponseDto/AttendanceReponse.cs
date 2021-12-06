using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.DTO.ResponseDto
{
    public class StudentActivitiesResponse
    {
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        public string gender { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string phoneNumber { get; set; }
        public int branchId { get; set; }
        public int classId { get; set; }
        public string className { get; set; }
        public int sectionId { get; set; }
        public string sectionName { get; set; }
        public int subjectId { get; set; }
        public string subjectName { get; set; }

    }
    public class AttendanceResponseModel : StudentActivitiesResponse 
    {
        public int scheduleId { get; set; }
        public int scheudleDetailId { get; set; }
        public bool present { get; set; }
        public bool permission { get; set; }
        public bool late { get; set; }

    }
    public class ResultResponseModel : StudentActivitiesResponse
    {
        public int assesmentId { get; set; }
        public decimal outOf { get; set; }
        public decimal score { get; set; }
        public bool isAbscent { get; set; }
        public bool isDisqualified { get; set; }
        public String disqualifiedReason { get; set; }
        public String assmentType { get; set; }

    }
}
