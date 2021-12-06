using SMS.SERVICE.DTO.ConfigurationManagmentDTO;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal.Response
{
    public class SubjectResponse : SCMSResponse
    {
        public SubjectModel subject { get; set; }
    }
    public class SubjectListResponse : SCMSResponse
    {
        public List<SubjectModel> subjects { get; set; }
    }
}
