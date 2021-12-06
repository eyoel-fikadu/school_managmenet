using SMS.SERVICE.DTO.AdmissionDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.DTO.ConfigurationManagmentDTO
{
    public class BranchDetailModel
    {
        public int BatchId { get; set; }
        public int BranchId { get; set; }
        public int SchoolId { get; set; }
        public String BranchName { get; set; }
        public bool IsActive { get; set; }
        public bool IsBranchMain { get; set; }
        public String SchoolName { get; set; }

        public List<ClassDetail> classDetails { get; set; }
                
    }
    public class ClassDetail
    {
        public String ClassName { get; set; }
        public int SectionId { get; set; }
        public String SectionName { get; set; }
        public int AssignedRoom { get; set; }
        public int SubjectID { get; set; }
        public int ClassID { get; set; }
        public String SubjectName { get; set; }
        public int EmployeeId { get; set; }
        public int TeacherId { get; set; }
        public String TeacherName { get; set; }
        public String TeacherType { get; set; }

    }
}
