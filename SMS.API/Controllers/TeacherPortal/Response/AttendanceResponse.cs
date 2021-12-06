using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.DTO.ResponseDto;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.TeacherPortal.Response
{
    public class AttendanceResponse : SCMSResponse
    {
        public AttendanceListModel attendance { get; set; } 
    }
    public class AttendanceListResponse : SCMSResponse
    {
        public List<AttendanceResponseModel> attendance { get; set; }
    }
}
