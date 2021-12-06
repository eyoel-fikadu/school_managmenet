using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SCMS.DataAccess.SCMS_Common;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.SecurityService;
using SMS.SERVICE.ServiceLayer.Security.ISecurityService;
using SMS.SERVICE.ServiceLayer.Security.Security_Models;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal.Action_Filters
{
    public class ItAdminAccessAPIFilter : IActionFilter
    {
        private JwtSettings jwt;
        private IItAdminSecurityService securityService;

        public ItAdminAccessAPIFilter(JwtSettings jwtSettings, IItAdminSecurityService securityServices)
        {
            this.jwt = jwtSettings;
            this.securityService = securityServices;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // our code before action executes
            EmployeeSecurityInfo info = (EmployeeSecurityInfo)securityService.GetMySecurityInfo(context.HttpContext.User);
            if (info == null)
            {
                context.Result = new ObjectResult(CommonMethods.GetException(CustomResponse.UNAUTHORIZED_USER_TRIES_TO_ACCESS));
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // our code after action executes
        }

    }
}
