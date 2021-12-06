using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.DTO.ResponseDto;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal.Response
{
    public class AssignedTeacherResponse : SCMSResponse
    {
        public TeacherModel teacher { get; set; }
    }
    public class AssignedTeacherListResponse : SCMSResponse
    {
        public List<TeacherModelResponse> teacher { get; set; }
    }
    public class SingleTeacherResponse : SCMSResponse
    {
        public TeacherModelResponse teacher { get; set; }
    }
}
