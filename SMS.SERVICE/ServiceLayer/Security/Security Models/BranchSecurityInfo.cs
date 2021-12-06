using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Security.Security_Models
{
    public class BranchSecurityInfo : SchoolSecuirtyInfo
    {
        public int BranchId { get; set; }
        public int BatchId { get; set; }
        public List<ClassSecurityInfo> classes { get; set; }

    }

    public class ClassSecurityInfo
    {
        public int classId { get; set; }
        public int sectionId { get; set; }
        public int subjectId { get; set; }
        public int employeeId { get; set; }
    }
}
