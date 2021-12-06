using SMS.SERVICE.DTO;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.UserPortal.Response
{
    public class UserReponse : SCMSResponse
    {
        public UserModel user { get; set; }
        public List<SCMSResponse> errors { get; set; }
    }
}
