using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.DTO.ResponseDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IStudentService
{
    public interface IAttendanceService
    {
        AttendanceListModel AddAttendance(AttendanceListModel attendance);
        List<AttendanceResponseModel> GetAttendances(int classId, int sectionId, int subjectId, int scheduleId, int scheduleDetailId, DateTime startDate, DateTime endDate, int studentId);
    }
}
