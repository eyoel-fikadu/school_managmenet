using SMS.API.WEB.Controllers.TeacherPortal.Response;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.StudentPortal.Response
{
    public class Student_AssesmentResponse : SCMSResponse
    {
        public AssesmentResponseModel assesment { get; set; }
    }
}
