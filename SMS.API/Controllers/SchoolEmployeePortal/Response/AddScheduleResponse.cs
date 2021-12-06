using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.DTO.ResponseDto;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal.Response
{
    public class AddScheduleResponse : SCMSResponse
    {
        public ScheduleModel scheduleModel { get; set; }
    }
    public class AddScheduleListResponse : SCMSResponse
    {
        public List<AddScheduleResponse> scheduleResponses { get; set; }
    }
    public class GetScheduleResponse : SCMSResponse
    {
        public ScheduleResponseModel scheduleModel { get; set; }
    }
    public class GetScheduleListResponse : SCMSResponse
    {
        public List<ScheduleResponseModel> scheduleModelList { get; set; }
    }
}
