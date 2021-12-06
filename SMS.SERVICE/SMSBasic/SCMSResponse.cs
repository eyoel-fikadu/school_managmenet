using Newtonsoft.Json;
using SMS.SERVICE.DTO.CommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.SMSBasic
{
    public class SCMSResponse
    {
        public int responseCode { get; set; }
        public String responseMessage { get; set; }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    public class SCMSErrorListResponse : SCMSResponse
    {
        public List<SCMSResponse> errors { get; set; }
        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    public class StringListResponse : SCMSResponse
    {
        public List<String> lookups { get; set; }
    }
}
