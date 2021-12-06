using SCMS.DataAccess;
using SMS.SERVICE.DTO.ClassActivityDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Internal.IClassActivity
{
    public interface ITimeTableServiceInternal
    {
        #region Schedule
        Schedule AddSchedule(ScheduleModel scheduleModel);
        Schedule UpdateSchedule(ScheduleModel scheduleModel);
        List<sp_GetScheduleListByBatchAndBranch_Result> GetSchedules(int batchId, int branchId, int timeTableId, int classId, int sectionId);
        List<sp_GetScheduleByEmployee_Result> GetSchedules(int batchId, int branchId, int employeeId);
        bool IsScheduleExist(int timeTableId, int classId, int sectionId);
        Schedule GetScheduleById(int scheduleId);
        Schedule GetScheduleByScheduleDetailId(int scheduleDetailId);

        #endregion

        #region Schdule detail

        ScheduleDetail AddScheduleDetail(ScheduleDetailModel scheduleDetailModel);
        ScheduleDetail UpdateScheduleDetail(ScheduleDetailModel scheduleDetailModel);
        List<ScheduleDetail> GetScheduleDetails(int scheduleId);
        #endregion

        #region Time Table

        TimeTable AddTimeTable(TimeTableModel timeTableModel);
        TimeTable UpdateTimeTable(TimeTableModel timeTableModel);
        List<TimeTable> GetActiveTimeTableByBatchAndByBranch(int batchId, int branchId);
        Schedule GetScheduleByTimeTableId(int timeTableId);
        #endregion
    }
}
