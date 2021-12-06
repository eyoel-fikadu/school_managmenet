using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal.Response
{
    public class TimeTableResponse : SCMSResponse
    {
        public TimeTableModel timeTable { get; set; }
    }
    public class TimeTableListResponse : SCMSResponse
    {
        public List<TimeTableModel> timeTableList { get; set; }
    }

}
