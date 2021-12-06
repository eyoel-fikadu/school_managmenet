using SMS.SERVICE.Service_Layer.IService.IAdmission;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.SecurityService;
using SMS.SERVICE.ServiceLayer.IService.IAdmission;
using SMS.SERVICE.ServiceLayer.Security.ISecurityService;
using SMS.SERVICE.ServiceLayer.Security.Security_Models;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Security.SecurityService
{
    public class StudentSecurityService : IStudentSecurityService
    {
        private readonly IUserServiceInternal userServiceInternal;
        private readonly IEnrollmentServiceInternal enrollmentServiceInternal;
        private readonly JwtSettings jwt;
        public StudentSecurityService()
        {
            userServiceInternal = Singleton.GetUserServiceInternal();
            enrollmentServiceInternal = Singleton.GetEnrollmentServiceInternal();
            jwt = Singleton.GetJwtSettings();
        }

        public PersonSecurityInfo GetMySecurityInfo(ClaimsPrincipal user)
        {
            int userId = jwt.GetCurrentUserId(user);
            var enrolledStudent = enrollmentServiceInternal.GetStudentByUserId(userId);
            if (enrolledStudent == null)
            {
                throw CommonMethods.GetException(CustomResponse.ERROR_RESPONSE_USER_DOESNT_EXIST);
            }
            var student = userServiceInternal.GetActiveStudentResult(userId);
            if (student == null)
            {
                throw CommonMethods.GetException(CustomResponse.STUDENT_DOESNT_EXIST);
            }
            StudentSecurityInfo info = new StudentSecurityInfo()
            {
                BatchId = student.BatchId,
                BranchId = student.BranchId,
                CalanderYearId = student.CalendarYearId,
                classId = student.ClassId,
                SchoolId = student.SchoolID,
                sectionId = CommonMethods.GetValue(student.SectionId),
                UserID = student.UserID,
                StudentId = student.EnrolledStudentId
            };
            return info;
        }
    }
}
