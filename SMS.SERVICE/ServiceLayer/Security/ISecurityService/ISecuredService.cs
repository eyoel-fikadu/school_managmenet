using SMS.SERVICE.ServiceLayer.Security.Security_Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Security.ISecurityService
{
    public interface ISecuredService
    {
        PersonSecurityInfo GetMySecurityInfo(ClaimsPrincipal user);
    }
}
