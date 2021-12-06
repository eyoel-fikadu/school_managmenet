using SCMS.DataAccess;
using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.IStudentServiceInternal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Internal.ServicesInternal.StudentServiceInternal
{
    public class AttendanceServiceInternal : IAttendanceServiceInternal
    {
        #region Attendance
        public Attendance AddAttendance(AttendanceModel attendanceModel)
        {
            Attendance attendance = new Attendance()
            {
                StudentId = attendanceModel.StudentId,
                IsActive = true,
                Late = attendanceModel.Late,
                Permission = attendanceModel.Permission,
                Present = attendanceModel.Present,
                ScheduleDetailId = attendanceModel.ScheduleDetailId,
                UpdatedDate = DateTime.Now,
                CreatedDate = DateTime.Now
            };
            return attendance;
        }

        public List<sp_GetAttendance_Result> GetAttendance(int classId, int sectionId, int subjectId, int scheduleId, int scheduleDetailId, DateTime startDate, DateTime endDate, int studentId)
        {
            using(SCMSEntities context = new SCMSEntities())
            {
                return context.sp_GetAttendance(classId, sectionId, subjectId, scheduleId, scheduleDetailId, startDate, endDate, studentId).ToList();
            }
        }

        public Attendance UpdateAttendance(AttendanceModel attendanceModel)
        {
            Attendance attendance = new Attendance()
            {
                StudentId = attendanceModel.StudentId,
                IsActive = attendanceModel.IsActive,
                Late = attendanceModel.Late,
                Permission = attendanceModel.Permission,
                Present = attendanceModel.Present,
                ScheduleDetailId = attendanceModel.ScheduleDetailId,
                UpdatedDate = DateTime.Now,
                AttendanceId = attendanceModel.AttendanceId
            };
            return attendance;
        }
        #endregion
    }
}
