using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal.Response
{
    public class Employee_ScheduleDetailResponse : SCMSResponse
    {
        public ScheduleDetailModel scheduleDetail { get; set; }
    }
    public class Employee_ScheduleDetailListResponse : SCMSResponse
    {
        public List<ScheduleDetailModel> scheduleDetail { get; set; }
    }
}
