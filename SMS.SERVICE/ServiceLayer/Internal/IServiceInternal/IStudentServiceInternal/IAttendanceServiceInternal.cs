using SCMS.DataAccess;
using SMS.SERVICE.DTO.ClassActivityDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.IStudentServiceInternal
{
    public interface IAttendanceServiceInternal
    {
        #region Attendance
        Attendance AddAttendance(AttendanceModel attendanceModel);
        Attendance UpdateAttendance(AttendanceModel attendanceModel);
        List<sp_GetAttendance_Result> GetAttendance(int classId, int sectionId, int subjectId, int scheduleId, int scheduleDetailId, DateTime startDate, DateTime endDate, int studentId);

        #endregion
    }
}
