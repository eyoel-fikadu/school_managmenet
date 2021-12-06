using SMS.SERVICE.DTO.ConfigurationManagmentDTO;
using SMS.SERVICE.DTO.ResponseDto;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.TeacherPortal.Response
{
    public class TeacherDetailResponse : SCMSResponse
    {
        public TeacherModelResponse teacher { get; set; }
    }
}
