using SMS.SERVICE.ServiceLayer.Security.Security_Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Security.ISecurityService
{
    public interface IItAdminSecurityService : ISecuredService
    {
        bool CanEmployeeAccessClassDataWithSubject(List<ClassSecurityInfo> classes, int classId, int sectionId, int subject);
        bool CanEmployeeAccessClass(List<ClassSecurityInfo> classes, int classId);
        bool isTimeTableAccessableByBranch(int batchId, int branchId, int timeTable);
        bool isScheduleAccessableByBranch(int branchId, int batchId, int scheduleId);
        bool isEmployeeAccessableByBranch(int branchId, int schoolId, int batchId, int employeeId);
        bool isTeacherAccessableByBranch(int branchId, int schoolId, int batchId, int employeeId);
        bool CanEmployeeAccessSection(List<ClassSecurityInfo> classes, int sectionId);
        bool CanEmployeeAccessClassAndSection(List<ClassSecurityInfo> classes, int classId, int sectionId);
        bool CanEmployeeAccessSubject(List<ClassSecurityInfo> classes, int subjectId);
    }
}
