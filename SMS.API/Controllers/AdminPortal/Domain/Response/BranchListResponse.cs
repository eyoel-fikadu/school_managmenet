using SMS.SERVICE.DTO.ConfigurationManagmentDTO;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.AdminPortal.Domain.Response
{
    public class Admin_BranchsListResponse : SCMSResponse
    {
        public List<BranchModel> branches { get; set; }
    }
    public class Admin_BranchResponse : SCMSResponse
    {
        public BranchModel branch { get; set; }
    }
}
