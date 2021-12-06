using SCMS.DataAccess;
using SMS.SERVICE.DTO;
using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.DTO.ResponseDto;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IStudentService;
using SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.IStudentServiceInternal;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.StudentService
{

    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceServiceInternal attendanceServiceInternal;

        public AttendanceService()
        {
            attendanceServiceInternal = Singleton.GetAttendanceServiceInternal();
        }
        public AttendanceListModel AddAttendance(AttendanceListModel attendance)
        {
            using(SCMSEntities context = new SCMSEntities())
            {
                foreach(AttendanceModel attendanceModel in attendance.attendanceList)
                {
                    Attendance att = attendanceServiceInternal.AddAttendance(attendanceModel);
                    context.Attendances.Add(att);
                }
                context.SaveChanges();
                return attendance;
            }
        }

        public List<AttendanceResponseModel> GetAttendances(int classId, int sectionId, int subjectId, int scheduleId, int scheduleDetailId, DateTime startDate, DateTime endDate, int studentId)
        {
            List<sp_GetAttendance_Result> results = attendanceServiceInternal.GetAttendance(classId, sectionId, subjectId, scheduleId, scheduleDetailId, startDate, endDate, studentId);
            return GetDtoModels.GetAttendanceResponse(results);
        }
    }
}
