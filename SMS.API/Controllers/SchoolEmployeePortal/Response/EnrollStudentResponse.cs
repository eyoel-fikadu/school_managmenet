using SMS.SERVICE.DTO;
using SMS.SERVICE.DTO.AdmissionDTO;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal.Response
{
    public class EnrollStudentResponse : SCMSResponse
    {
        public EnrollSingleStudentModel student { get; set; }
    }
    public class EnrollStudentListResponse : SCMSResponse
    {
        public List<EnrollSingleStudentModel> student { get; set; }
    }
    public class EnrollMultipleStudentResponse : SCMSResponse
    {
        public EnrolledMultipleStudentModel student { get; set; }
    }
    public class UserStudentResponse : SCMSResponse
    {
        public UserModel user { get; set; }
        public EnrollSingleStudentModel enrollStudent { get; set; }
        public List<SCMSResponse> errors { get; set; }

    }
}
