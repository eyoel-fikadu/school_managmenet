using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.DTO.ResponseDto
{
    public class UserModelResponse
    {
        public int UserId { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String LastName { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }
        public String Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsUserActive { get; set; }
        public bool IsUserEnabled { get; set; }
    }
    public class EmployeeModelResponse : UserModelResponse
    {
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String EmployeeType { get; set; }
        public bool IsEmployeeActive { get; set; }
    }
    public class TeacherModelResponse : EmployeeModelResponse
    {
        public List<ClassInformation> classInformation { get; set; }
    }
    public class ClassInformation
    {
        public int classId { get; set; }
        public String className { get; set; }
        public int sectionId { get; set; }
        public String sectionName { get; set; }
        public int subjectId { get; set; }
        public String subjectName { get; set; }
        public String TeacherType { get; set; }
        public DateTime classStartDate { get; set; }
        public DateTime classEndDate { get; set; }
        public bool IsClassActive { get; set; }
    }
}
