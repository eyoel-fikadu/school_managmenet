using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Security.Security_Models
{
    public class TeacherSecurityInfo : EmployeeSecurityInfo
    {
        public List<ClassInfo> classInfos { get; set; }
    }

    public class ClassInfo
    {
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int SubjectId { get; set; }
        public String TeacherType { get; set; }
    }
}
