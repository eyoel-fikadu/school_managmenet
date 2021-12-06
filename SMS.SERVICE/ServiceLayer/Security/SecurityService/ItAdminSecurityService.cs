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
    public class ItAdminSecurityService : IItAdminSecurityService 
    {
        private readonly IUserServiceInternal userServiceInternal;
        private readonly IEnrollmentServiceInternal enrollmentServiceInternal;
        private readonly ISchoolServiceInternal schoolServiceInternal;
        private readonly IMainSystemServiceInternal mainSystemServiceInternal;
        private readonly ITimeTableServiceInternal timeTableService;
        private readonly JwtSettings jwt;
        public ItAdminSecurityService()
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
            else if (employee.EmployeeTypeId == ConstantValues.LOOKUP_VALUE_NAMESPACE_IT_STAFF)
            {
                ItAdminSecurityInfo itAdmin = new ItAdminSecurityInfo()
                {
                    BatchId = employee.BatchId,
                    BranchId = employee.BranchId,
                    SchoolId = employee.SchoolID,
                    EmployeeType = employee.EmployeeTypeId,
                    IsEmailVerified = employee.EmailVerified,
                    IsPhoneVerified = employee.PhoneNumberVerified,
                    IsEnabled = employee.IsEnabled,
                    IsLocked = employee.IsLocked,
                    NameSpace = employee.NameSpace,
                    PublicId = employee.PublicId,
                    UserID = employee.UserID,
                    CalanderYearId = employee.CalendarYearId,
                    EmployeeId = employee.EmployeeId
                };
                var classes = enrollmentServiceInternal.GetAssignedTeachers(employee.BatchId, employee.SchoolID, employee.BranchId, 0);
                if (classes != null)
                {
                    List<ClassSecurityInfo> infos = new List<ClassSecurityInfo>();
                    foreach (var cls in classes)
                    {
                        ClassSecurityInfo info = new ClassSecurityInfo()
                        {
                            classId = CommonMethods.GetValue(cls.ClassId),
                            sectionId = CommonMethods.GetValue(cls.SectionId),
                            subjectId = CommonMethods.GetValue(cls.SubjectId),
                            employeeId = CommonMethods.GetValue(cls.EmployeeId)
                        };
                        infos.Add(info);
                    }
                    itAdmin.classes = infos;
                }
                return itAdmin;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        public bool CanEmployeeAccessClassDataWithSubject(List<ClassSecurityInfo> classes, int classId, int sectionId, int subject)
        {
            if(classes == null)
            {
                throw CommonMethods.GetException(CustomResponse.CLASS_INFORMATION_IS_NOT_PROVIDED);
            }
            var accesedClass = classes.FirstOrDefault(x => x.classId == classId && x.sectionId == sectionId && x.subjectId == subject);
            if (accesedClass == null)
            {
                throw CommonMethods.GetException(CustomResponse.CLASS_IS_NOT_ACCESSED_BY_EMPLOYEE);
            }
            return true;
        }
        public bool CanEmployeeAccessClass(List<ClassSecurityInfo> classes, int classId)
        {
            if (classes == null)
            {
                throw CommonMethods.GetException(CustomResponse.CLASS_INFORMATION_IS_NOT_PROVIDED);
            }
            var accesedClass = classes.FirstOrDefault(x => x.classId == classId);
            if (accesedClass == null)
            {
                throw CommonMethods.GetException(CustomResponse.CLASS_IS_NOT_ACCESSED_BY_EMPLOYEE);
            }
            return true;
        }
        public bool CanEmployeeAccessSection(List<ClassSecurityInfo> classes, int sectionId)
        {
            if (classes == null)
            {
                throw CommonMethods.GetException(CustomResponse.CLASS_INFORMATION_IS_NOT_PROVIDED);
            }
            var accesedClass = classes.FirstOrDefault(x => x.sectionId == sectionId);
            if (accesedClass == null)
            {
                throw CommonMethods.GetException(CustomResponse.CLASS_IS_NOT_ACCESSED_BY_EMPLOYEE);
            }
            return true;
        }
        public bool isTimeTableAccessableByBranch(int batchId, int branchId, int timeTable)
        {
            var timeTableList = timeTableService.GetActiveTimeTableByBatchAndByBranch(batchId, branchId);
            if (timeTableList != null)
            {
                List<int> timeTableIds = timeTableList.Select(x => x.TimeTableId).ToList();
                if (timeTableIds.Contains(timeTable)) return true;
            }
            return false;
        }
        public bool isScheduleAccessableByBranch(int branchId, int batchId, int scheduleId)
        {
            var schedule = timeTableService.GetScheduleById(scheduleId);
            if (schedule != null)
            {
                var timeTable = timeTableService.GetActiveTimeTableByBatchAndByBranch(batchId, branchId);
                if (timeTable != null)
                {
                    List<int> timeTableIds = timeTable.Select(x => x.TimeTableId).ToList();
                    if (timeTableIds.Contains(schedule.TimeTableId)) return true;
                }
            }
            return false;
        }
        public bool isEmployeeAccessableByBranch(int branchId, int schoolId, int batchId, int employeeId)
        {
            var employee = enrollmentServiceInternal.GetEmployeeByBranch(batchId, schoolId, branchId, employeeId);
            if (employee == null) return false;
            return true;
        }
        public bool isTeacherAccessableByBranch(int branchId, int schoolId, int batchId, int employeeId)
        {
            var employee = enrollmentServiceInternal.GetEmployeeByBranch(batchId, schoolId, branchId, employeeId);
            if (employee == null) return false;
            if (employee.EmployeeTypeId != ConstantValues.LOOKUP_VALUE_NAMESPACE_TEACHER) return false;
            return true;
        }
        public bool CanEmployeeAccessSubject(List<ClassSecurityInfo> classes, int subjectId)
        {
            if (classes == null)
            {
                throw CommonMethods.GetException(CustomResponse.CLASS_INFORMATION_IS_NOT_PROVIDED);
            }
            var accesedClass = classes.FirstOrDefault(x => x.subjectId == subjectId);
            if (accesedClass == null)
            {
                throw CommonMethods.GetException(CustomResponse.CLASS_IS_NOT_ACCESSED_BY_EMPLOYEE);
            }
            return true;
        }
        public bool CanEmployeeAccessClassAndSection(List<ClassSecurityInfo> classes, int classId, int sectionId)
        {
            if (classes == null)
            {
                throw CommonMethods.GetException(CustomResponse.CLASS_INFORMATION_IS_NOT_PROVIDED);
            }
            var accesedClass = classes.FirstOrDefault(x => x.classId == classId && x.sectionId == sectionId);
            if (accesedClass == null)
            {
                throw CommonMethods.GetException(CustomResponse.CLASS_IS_NOT_ACCESSED_BY_EMPLOYEE);
            }
            return true;
        }
    }
}
