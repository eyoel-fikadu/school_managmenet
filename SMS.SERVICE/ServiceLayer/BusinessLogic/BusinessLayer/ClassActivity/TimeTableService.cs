using SCMS.DataAccess;
using SMS.SERVICE.DTO;
using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.DTO.CommonDTO;
using SMS.SERVICE.DTO.ResponseDto;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IClassActivity;
using SMS.SERVICE.ServiceLayer.Internal.IClassActivity;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.ClassActivity
{
    public class TimeTableService : ITimeTableService
    {
        private readonly ITimeTableServiceInternal timeTableServiceInternal;
        public TimeTableService()
        {
            timeTableServiceInternal = Singleton.GetTimeTableServiceInternal();
        }
        #region TIme Table
        public TimeTableModel AddTimeTable(TimeTableModel model)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                TimeTable timeTableInserted = timeTableServiceInternal.AddTimeTable(model);
                timeTableInserted = context.TimeTables.Add(timeTableInserted);
                context.SaveChanges();
                model.TimeTableId = timeTableInserted.TimeTableId;
                return model;
            }
        }
        public List<TimeTableModel> GetAllTimeTablePerBranch(int batchId, int branchId)
        {
            return GetDtoModels.GetTimeTable(timeTableServiceInternal.GetActiveTimeTableByBatchAndByBranch(batchId, branchId));
        }
        public List<TimeTableModel> GetAllTimeTablePerDayForBranch(int batchId, int branchId, string dayOfWeek)
        {
            List<TimeTable> timeTables = timeTableServiceInternal.GetActiveTimeTableByBatchAndByBranch(batchId, branchId).Where(x => x.DayOfTheWeekId == dayOfWeek).ToList();
            return GetDtoModels.GetTimeTable(timeTables);
        }

        #endregion

        #region Schedule
        public ScheduleModel AddSchedule(ScheduleModel model)
        {
            if(timeTableServiceInternal.IsScheduleExist(model.TimeTableId,model.ClassId, model.SectionId))
            {
                throw CommonMethods.GetException(CustomResponse.SCHDEULE_ALREADY_EXISTS);
            }
            else
            {
                using (SCMSEntities context = new SCMSEntities())
                {
                    Schedule scheduleInserted = timeTableServiceInternal.AddSchedule(model);
                    scheduleInserted = context.Schedules.Add(scheduleInserted);
                    context.SaveChanges();
                    model.ScheduleId = scheduleInserted.ScheduleId;
                    return model;
                }
            }
        }
        public List<ScheduleResponseModel> GetSchedulePerClass(int batchId, int branchId, int timeTableId, int classId, int sectionId)
        {
            var result = timeTableServiceInternal.GetSchedules(batchId, branchId, timeTableId, classId, sectionId);
            return GetDtoModels.GetScheduleModelList(result);
        }
        public List<ScheduleResponseModel> GetSchedulePerBranch(int batchId, int branchId)
        {
            var result = timeTableServiceInternal.GetSchedules(batchId, branchId,0, 0, 0);
            return GetDtoModels.GetScheduleModelList(result);
        }
        public ScheduleModel GetScheduleById(int scheduleId)
        {
            return GetDtoModels.GetScheduleModel(timeTableServiceInternal.GetScheduleById(scheduleId));
        }
        public ScheduleModel GetScheduleByScheduleDetailId(int scheduleDetailId)
        {
            return GetDtoModels.GetScheduleModel(timeTableServiceInternal.GetScheduleByScheduleDetailId(scheduleDetailId));
        }
        public List<ScheduleResponseModel> GetScheduleByEmployee(int batchId, int branchId, int employeeId)
        {
            var result = timeTableServiceInternal.GetSchedules(batchId, branchId, employeeId);
            return GetDtoModels.GetScheduleModelList(result);
        }
        #endregion

        #region Schdule detail
        public ScheduleDetailModel AddScheduleDetail(ScheduleDetailModel model)
        {
            var schedule = timeTableServiceInternal.GetScheduleDetails(model.ScheduleId);
            if(schedule != null && schedule.Count > 0)
            {
                if (schedule.Where(x => x.Date == model.Date).FirstOrDefault() != null)
                {
                    throw CommonMethods.GetException(CustomResponse.SCHEDULE_DETAIL_PER_SCHEDULE_ALREADY_ADDED);
                }
            }
            using (SCMSEntities context = new SCMSEntities())
            {
                ScheduleDetail scheduleDetail = timeTableServiceInternal.AddScheduleDetail(model);
                scheduleDetail = context.ScheduleDetails.Add(scheduleDetail);
                context.SaveChanges();
                model.ScheduleDetailId = scheduleDetail.ScheduleDetailId;
                return model;
            }
        }
        public List<ScheduleDetailModel> GetScheduleDetails(int scheduleId)
        {
            return GetDtoModels.GetScheduleDetailModelList(timeTableServiceInternal.GetScheduleDetails(scheduleId));
        }
        #endregion
    }
}
