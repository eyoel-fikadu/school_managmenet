using SMS.SERVICE.DTO.ResponseDto;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.TeacherPortal.Response
{
    public class Teacher_GetScheduleListResponse : SCMSResponse
    {
        public List<ScheduleResponseModel> scheduleModelList { get; set; }
    }
}
