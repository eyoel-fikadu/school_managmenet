using SMS.SERVICE.DTO.AdmissionDTO;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal.Response
{
    public class SectionResponse : SCMSResponse
    {
        public SectionModel section { get; set; }
    }
    public class SectionListResponse : SCMSResponse
    {
        public List<SectionModel> sections { get; set; }
    }
}
