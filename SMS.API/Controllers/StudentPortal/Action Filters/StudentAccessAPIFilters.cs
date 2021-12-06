using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.SecurityService;
using SMS.SERVICE.ServiceLayer.Security.ISecurityService;
using SMS.SERVICE.ServiceLayer.Security.Security_Models;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.StudentPortal.Action_Filters
{
    public class StudentAccessAPIFilters : IActionFilter
    {
        private JwtSettings jwt;
        private IStudentSecurityService securityService;

        public StudentAccessAPIFilters(JwtSettings jwtSettings, IStudentSecurityService securityServices)
        {
            this.jwt = jwtSettings;
            this.securityService = securityServices;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // our code before action executes
            StudentSecurityInfo info = (StudentSecurityInfo)securityService.GetMySecurityInfo(context.HttpContext.User);
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
