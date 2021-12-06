using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.TeacherPortal.Response
{
    public class Teacher_ScheduleDetailResponse : SCMSResponse
    {
        public ScheduleDetailModel scheduleDetail { get; set; }
    }
    public class Teacher_ScheduleDetailListResponse : SCMSResponse
    {
        public List<ScheduleDetailModel> scheduleDetail { get; set; }
    }
}
