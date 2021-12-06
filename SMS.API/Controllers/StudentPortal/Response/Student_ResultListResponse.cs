using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.DTO.ResponseDto;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.StudentPortal.Response
{
    public class Student_ResultListResponse : SCMSResponse
    {
        public List<ResultResponseModel> results { get; set; }
    }
    public class Student_ResultResponse : SCMSResponse
    {
        public ResultModelList result { get; set; }
    }
}
