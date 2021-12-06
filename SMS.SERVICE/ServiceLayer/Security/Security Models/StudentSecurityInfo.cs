using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Security.Security_Models
{
    public class StudentSecurityInfo : PersonSecurityInfo
    {
        public int SchoolId { get; set; }
        public int BatchId { get; set; }
        public int BranchId { get; set; }
        public int CalanderYearId { get; set; }
        public int StudentId { get; set; }
        public int classId { get; set; }
        public int sectionId { get; set; }
    }
}
