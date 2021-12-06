using SCMS.DataAccess;
using SMS.SERVICE.DTO.ClassActivityDTO;
using SMS.SERVICE.ServiceLayer.Internal.IClassActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Internal.ServicesInternal.ClassActivityInternal
{
    public class TimeTableServiceInternal : ITimeTableServiceInternal
    {
        #region Time Table
        public TimeTable AddTimeTable(TimeTableModel model)
        {
            TimeTable timeTable = new TimeTable()
            {
                BatchId = model.BatchId,
                BranchId = model.BranchId,
                CreatedDate = DateTime.Now,
                DayOfTheWeekId = model.DayOfTheWeek,
                Description = model.Description,
                EndTime = model.EndTime.TimeOfDay,
                IsActive = true,
                StartTime = model.StartTime.TimeOfDay,
                UpdatedDate = DateTime.Now,
                CreatedBy = model.createdBy,
                UpdatedBy = model.updatedBy
            };

            return timeTable;
        }
        public TimeTable UpdateTimeTable(TimeTableModel model)
        {
            TimeTable timeTable = new TimeTable()
            {
                BatchId = model.BatchId,
                BranchId = model.BranchId,
                DayOfTheWeekId = model.DayOfTheWeek,
                Description = model.Description,
                EndTime = model.EndTime.TimeOfDay,
                IsActive = model.IsActive,
                StartTime = model.StartTime.TimeOfDay,
                UpdatedDate = DateTime.Now,
                TimeTableId = model.TimeTableId
            };

            return timeTable;
        }
        public List<TimeTable> GetActiveTimeTableByBatchAndByBranch(int batchId, int branchId)
        {
            using(SCMSEntities context = new SCMSEntities())
            {
                return context.TimeTables.Where(x => x.BatchId == batchId && x.BranchId == branchId).ToList();
            }
        }
        #endregion

        #region Schedule
        public Schedule AddSchedule(ScheduleModel model)
        {
            Schedule schedule = new Schedule()
            {
                ClassId = model.ClassId,
                CreatedDate = DateTime.Now,
                IsActive = true,
                SectionId = model.SectionId,
                SubjectId = model.SubjectId,
                TimeTableId = model.TimeTableId,
                UpdatedDate = DateTime.Now,
                CreatedBy = model.createdBy,
                UpdatedBy = model.updatedBy
            };

            return schedule;
        }
        public Schedule UpdateSchedule(ScheduleModel model)
        {
            Schedule schedule = new Schedule()
            {
                ClassId = model.ClassId,
                IsActive = model.IsActive,
                ScheduleId = model.ScheduleId,
                SectionId = model.SectionId,
                SubjectId = model.SubjectId,
                TimeTableId = model.TimeTableId,
                UpdatedDate = DateTime.Now
            };

            return schedule;
        }
        public List<sp_GetScheduleListByBatchAndBranch_Result> GetSchedules(int batchId, int branchId, int timeTableId, int classId, int sectionId)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.sp_GetScheduleListByBatchAndBranch(batchId, branchId, timeTableId, classId, sectionId).ToList();
            }
        }
        public List<sp_GetScheduleByEmployee_Result> GetSchedules(int batchId, int branchId, int employeeId)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.sp_GetScheduleByEmployee(batchId, branchId, employeeId).ToList();
            }
        }
        public Schedule GetScheduleByTimeTableId(int timeTableId)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.Schedules.Where(x => x.TimeTableId == timeTableId).FirstOrDefault();
            }
        }
        public Schedule GetScheduleById(int scheduleId)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.Schedules.Where(x => x.ScheduleId == scheduleId).FirstOrDefault();
            }
        }
        public bool IsScheduleExist(int timeTableId, int classId, int sectionId)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                var schedule = context.Schedules.Where(x => x.TimeTableId == timeTableId && x.ClassId == classId
                && x.SectionId == sectionId).FirstOrDefault();
                if (schedule == null) return false;
                return true;
            }
        }
        public Schedule GetScheduleByScheduleDetailId(int scheduleDetailId)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                var scheduleDetail = context.ScheduleDetails.Where(x => x.ScheduleDetailId == scheduleDetailId).FirstOrDefault();
                if(scheduleDetail != null)
                {
                    return context.Schedules.Find(scheduleDetail.Schedule);
                }
                return null;
            }
        }
        #endregion

        #region Schedule Detail
        public ScheduleDetail AddScheduleDetail(ScheduleDetailModel model)
        {
            ScheduleDetail scheduleDetail = new ScheduleDetail()
            {
                Schedule = model.ScheduleId,
                IsActive = true,
                UpdatedDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                EndTime = model.EndTime.TimeOfDay,
                StartTime = model.StartTime.TimeOfDay,
                TeacherId = model.TeacherId,
                Date = model.Date,
                UpdatedBy = model.updatedBy,
                CreatedBy = model.createdBy
            };
            
            return scheduleDetail;
        }
        public ScheduleDetail UpdateScheduleDetail(ScheduleDetailModel model)
        {
            ScheduleDetail scheduleDetail = new ScheduleDetail()
            {
                Schedule = model.ScheduleId,
                IsActive = true,
                UpdatedDate = DateTime.Now,
                EndTime = model.EndTime.TimeOfDay,
                StartTime = model.StartTime.TimeOfDay,
                TeacherId = model.TeacherId,
                Date = model.Date,
                ScheduleDetailId = model.ScheduleDetailId
            };

            return scheduleDetail;
        }
        public List<ScheduleDetail> GetScheduleDetails(int scheduleId)
        {
            using(SCMSEntities context = new SCMSEntities())
            {
                return context.ScheduleDetails.Where(x => x.Schedule == scheduleId).ToList();
            }
        }
        #endregion

    }
}
