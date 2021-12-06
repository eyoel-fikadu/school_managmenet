using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.DTO.ResponseDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IClassActivity
{
    public interface ITimeTableService
    {
        #region Schdule
        ScheduleModel AddSchedule(ScheduleModel scheduleModel);
        List<ScheduleResponseModel> GetSchedulePerClass(int batchId, int branchId, int timeTableId, int classId, int sectionId);
        List<ScheduleResponseModel> GetSchedulePerBranch(int batchId, int branchId);
        List<ScheduleResponseModel> GetScheduleByEmployee(int batchId, int branchId, int employeeId);
        ScheduleModel GetScheduleById(int scheduleId);
        ScheduleModel GetScheduleByScheduleDetailId(int scheduleDetailId);

        #endregion

        #region Schdule detail
        ScheduleDetailModel AddScheduleDetail(ScheduleDetailModel model);
        List<ScheduleDetailModel> GetScheduleDetails(int scheduleId);
        #endregion

        #region TIme Table
        TimeTableModel AddTimeTable(TimeTableModel timeTableModel);
        List<TimeTableModel> GetAllTimeTablePerBranch(int batchId, int branchId);
        List<TimeTableModel> GetAllTimeTablePerDayForBranch(int batchId, int branchId, String dayOfWeek);

        #endregion
    }
}
