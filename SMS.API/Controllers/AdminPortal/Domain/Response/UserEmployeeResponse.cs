using SMS.SERVICE.DTO;
using SMS.SERVICE.DTO.AdmissionDTO;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.AdminPortal.Domain.Response
{
    public class Admin_UserEmployeeResponse : SCMSResponse
    {
        public UserModel user { get; set; }
        public EmployeeModel employee { get; set; }
        public List<SCMSResponse> errors { get; set; }

    }
}
