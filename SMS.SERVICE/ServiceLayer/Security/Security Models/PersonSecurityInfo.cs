using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Security.Security_Models
{
    public class PersonSecurityInfo
    {
        public int UserID { get; set; }
        public String NameSpace { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsLocked { get; set; }
        public String PublicId { get; set; }
        public bool IsPhoneVerified { get; set; }
        public bool IsEmailVerified { get; set; }
    }
}
