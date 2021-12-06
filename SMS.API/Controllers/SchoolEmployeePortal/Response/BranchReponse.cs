using SMS.SERVICE.DTO.ConfigurationManagmentDTO;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal.Response
{
    public class BranchResponse : SCMSResponse
    {
        public BranchModel branch { get; set; }
    }
    public class BranchListResponse : SCMSResponse
    {
        public List<BranchModel> branches { get; set; }
    }
}
