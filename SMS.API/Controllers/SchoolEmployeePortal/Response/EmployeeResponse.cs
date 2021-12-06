using SMS.SERVICE.DTO;
using SMS.SERVICE.DTO.AdmissionDTO;
using SMS.SERVICE.DTO.ResponseDto;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal.Response
{
    public class EmployeeResponse : SCMSResponse
    {
        public EmployeeModel employee { get; set; } 
    }
    public class UserEmployeeResponse : SCMSResponse
    {
        public UserModel user { get; set; }
        public EmployeeModel employee { get; set; }
        public List<SCMSResponse> errors { get; set; }

    }
    public class EmployeeListResponse : SCMSResponse
    {
        public List<EmployeeModelResponse> employee { get; set; }
    }
}
