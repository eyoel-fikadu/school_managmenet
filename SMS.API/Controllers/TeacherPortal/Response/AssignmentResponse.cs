using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.TeacherPortal.Response
{
    public class Teacher_AssignmentResponse : SCMSResponse
    {
        public AssignmentModel assignment { get; set; }
    }
}
