using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.SMSBasic
{
    public class SCMSException : Exception
    {
        public String responseMessage { get; set; }
        public int responseCode { get; set; }
    }
    public class SCSMExceptionList : Exception
    {
        public List<SCMSException> errorMessages { get; set; }
    }

    public class SCMSExceptionListResponse : SCMSResponse
    {
        public List<SCMSException> errorList { get; set; }
    }
}
