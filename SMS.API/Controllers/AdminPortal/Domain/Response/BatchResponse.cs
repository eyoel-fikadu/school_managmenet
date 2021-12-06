using SMS.SERVICE.DTO.AdmissionDTO;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.AdminPortal.Domain.Response
{
    public class BatchListResponse : SCMSResponse
    {
        public List<BatchModel> batches { get; set; }
    }
    public class BatchResponse : SCMSResponse
    {
        public BatchModel batches { get; set; }
    }
    public class RegisteredBatchResponse : SCMSResponse
    {
        public List<RegisteredBranchForTheBatch> branches { get; set; }
    }

}
