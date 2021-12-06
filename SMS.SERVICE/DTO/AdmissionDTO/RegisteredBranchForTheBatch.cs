using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.DTO.AdmissionDTO
{
    public class RegisteredBranchForTheBatch
    {
        public int batchId { get; set; }
        public int calendarYearId { get; set; }
        public DateTime calendarYearStartDate { get; set; }
        public DateTime calendarYearEndDate { get; set; }
        public int branchId { get; set; }
        public String branchName { get; set; }
        public int schoolId { get; set; }
        public String schoolName { get; set; }
        public bool isBranchActive { get; set; }
        public bool isBatchActive { get; set; }
        public bool isSchoolActive { get; set; }
        public bool isMainBranch { get; set; }
    }
}
