using SCMS.DataAccess.SCMS_Common;
using SMS.SERVICE.Service_Layer.IService.IAdmission;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.SecurityService;
using SMS.SERVICE.ServiceLayer.Internal.IClassActivity;
using SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.IConfigurationManagmentInternal;
using SMS.SERVICE.ServiceLayer.IService.IAdmission;
using SMS.SERVICE.ServiceLayer.IService.IConfigurationManagment;
using SMS.SERVICE.ServiceLayer.Security.ISecurityService;
using SMS.SERVICE.ServiceLayer.Security.Security_Models;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Security.SecurityService
{
    public class TeacherSecurityService : ITeacherSecurityService
    {
        private readonly IUserServiceInternal userServiceInternal;
        private readonly IEnrollmentServiceInternal enrollmentServiceInternal;
        private readonly ISchoolServiceInternal schoolServiceInternal;
        private readonly IMainSystemServiceInternal mainSystemServiceInternal;
        private readonly ITimeTableServiceInternal timeTableService;
        private readonly JwtSettings jwt;
        public TeacherSecurityService()
        {
            userServiceInternal = Singleton.GetUserServiceInternal();
            enrollmentServiceInternal = Singleton.GetEnrollmentServiceInternal();
            schoolServiceInternal = Singleton.GetSchoolServiceInternal();
            mainSystemServiceInternal = Singleton.GetMainSystemServiceInternal();
            timeTableService = Singleton.GetTimeTableServiceInternal();
            jwt = Singleton.GetJwtSettings();
        }
        public PersonSecurityInfo GetMySecurityInfo(ClaimsPrincipal user)
        {
            int userId = jwt.GetCurrentUserId(user);
            var employeeBranch = enrollmentServiceInternal.GetEmployeeByUserId(userId);
            if (employeeBranch == null)
            {
                throw CommonMethods.GetException(CustomResponse.ERROR_RESPONSE_USER_DOESNT_EXIST);
            }

            int branchId = employeeBranch.BranchId;
            if (!mainSystemServiceInternal.isBranchActiveOnBatch(branchId))
            {
                throw CommonMethods.GetException(CustomResponse.BRANCH_IS_NOT_ADDED_TO_CALENDAR_YEAR);
            }

            var employee = userServiceInternal.GetActiveEmployeResult(userId);
            if (employee == null)
            {
                throw CommonMethods.GetException(CustomResponse.EMPLOYEE_IS_NOT_ADDED_TO_CURRENT_CALENDAR_YEAR);
            }
            else if (!employee.isEmployeeActive)
            {
                throw CommonMethods.GetException(CustomResponse.EMPLOYEE_IS_NOT_ACTIVE);
            }
            else if (employee.EmployeeTypeId == ConstantValues.LOOKUP_VALUE_NAMESPACE_TEACHER)
            {
                TeacherSecurityInfo employeeSecurity = new TeacherSecurityInfo()
                {
                    BatchId = employee.BatchId,
                    BranchId = employee.BranchId,
                    EmployeeType = employee.EmployeeTypeId,
                    IsEmailVerified = employee.EmailVerified,
                    IsPhoneVerified = employee.PhoneNumberVerified,
                    IsEnabled = employee.IsEnabled,
                    IsLocked = employee.IsLocked,
                    NameSpace = employee.NameSpace,
                    PublicId = employee.PublicId,
                    UserID = employee.UserID,
                    EmployeeId = employee.EmployeeId,
                    CalanderYearId = employee.CalendarYearId,
                    SchoolId = employee.SchoolID
                };

                var classes = enrollmentServiceInternal.GetAssignedTeachers(employee.BatchId, employee.SchoolID, employee.BranchId, employee.EmployeeId);
                if (classes != null)
                {
                    List<ClassInfo> infos = new List<ClassInfo>();
                    foreach (var cls in classes)
                    {
                        ClassInfo info = new ClassInfo()
                        {
                            ClassId = CommonMethods.GetValue(cls.ClassId),
                            SectionId = CommonMethods.GetValue(cls.SectionId),
                            SubjectId = CommonMethods.GetValue(cls.SubjectId),
                            TeacherType = cls.TeacherTypeId
                        };
                        infos.Add(info);
                    }
                    employeeSecurity.classInfos = infos;
                }
                return employeeSecurity;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
      
        #region Teacher
        public bool CanTeacherAccessClassData(List<ClassInfo> classes, int classId, int sectionId)
        {
            var accesedClass = classes.FirstOrDefault(x => x.ClassId == classId && x.SectionId == sectionId);
            if (accesedClass == null)
            {
                throw CommonMethods.GetException(CustomResponse.CLASS_IS_NOT_ACCESSED_BY_EMPLOYEE);
            }
            return true;
        }
        public bool CanTeacherAccessClassDataWithSubject(List<ClassInfo> classes, int classId, int sectionId, int subject)
        {
            var accesedClass = classes.FirstOrDefault(x => x.ClassId == classId && x.SectionId == sectionId && x.SubjectId == subject);
            if (accesedClass == null)
            {
                throw CommonMethods.GetException(CustomResponse.CLASS_IS_NOT_ACCESSED_BY_EMPLOYEE);
            }
            return true;
        }
        public bool CanTeacherAccessSchedule(int batchId, int branchId, int scheduleId, int employeeId)
        {
            List<int> schedules = timeTableService.GetSchedules(batchId, branchId, employeeId).Select(x => x.ScheduleId).ToList();
            return schedules.Contains(scheduleId);

        }
        #endregion
        public bool AreStudentsPartOfClassSection(List<int> studentIds, int calendarYearId, int schoolId, int branchId, int classId, int sectionId)
        {
            List<int> students = enrollmentServiceInternal.GetStudentList(calendarYearId, schoolId, branchId, classId, sectionId, 0).Select(x => x.EnrolledStudentId).ToList();
            return CommonMethods.CompareList(students, studentIds);
        }
    }
}
