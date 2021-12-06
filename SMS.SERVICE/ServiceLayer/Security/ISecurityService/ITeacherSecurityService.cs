using SMS.SERVICE.ServiceLayer.Security.Security_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Security.ISecurityService
{
    public interface ITeacherSecurityService : ISecuredService
    {
        #region Teacher
        bool CanTeacherAccessClassData(List<ClassInfo> classes, int classId, int sectionId);
        bool CanTeacherAccessClassDataWithSubject(List<ClassInfo> classes, int classId, int sectionId, int subject);
        bool CanTeacherAccessSchedule(int batchId, int branchId, int scheduleId, int employeeId);
        #endregion
        bool AreStudentsPartOfClassSection(List<int> studentIds, int calendarYearId, int schoolId, int branchId, int classId, int sectionId);
    }
}
