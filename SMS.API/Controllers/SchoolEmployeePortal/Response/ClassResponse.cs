using SMS.SERVICE.DTO.AdmissionDTO;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal.Response
{
    public class ClassResponse : SCMSResponse
    {
        public ClassModel Class { get; set; }
    }
    public class ClassListResponse : SCMSResponse
    {
        public List<ClassModel> classes { get; set; }
    }
}
