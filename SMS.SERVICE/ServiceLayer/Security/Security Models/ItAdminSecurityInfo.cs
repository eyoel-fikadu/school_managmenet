using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Security.Security_Models
{
    public class ItAdminSecurityInfo : EmployeeSecurityInfo
    {
        public List<ClassSecurityInfo> classes { get; set; }
    }
}
