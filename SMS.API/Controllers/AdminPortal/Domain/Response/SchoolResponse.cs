using SMS.SERVICE.DTO.ConfigurationManagmentDTO;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.AdminPortal.Domain.Response
{
    public class SchoolResponse : SCMSResponse
    {
        public SchoolModel school { get; set; }
        public List<SCMSResponse> errors { get; set; }
    }
    public class SchoolListResponse : SCMSResponse
    {
        public List<SchoolModel> schools { get; set; }
    }
}
