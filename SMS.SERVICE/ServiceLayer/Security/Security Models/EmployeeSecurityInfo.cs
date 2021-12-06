using System;

namespace SMS.SERVICE.ServiceLayer.Security.Security_Models
{
    public class EmployeeSecurityInfo : PersonSecurityInfo
    {
        public int SchoolId { get; set; }
        public int BatchId { get; set; }
        public int BranchId { get; set; }
        public int CalanderYearId { get; set; }
        public String EmployeeType { get; set; }
        public int EmployeeId { get; set; }
    }
}
