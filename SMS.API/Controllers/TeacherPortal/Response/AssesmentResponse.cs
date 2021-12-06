using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.TeacherPortal.Response
{
    public class AssesmentResponse : SCMSResponse
    {
        public AssesmentResponseModel assesment { get; set; }
    }
}
