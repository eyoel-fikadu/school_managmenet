using SCMS.DataAccess;
using SMS.SERVICE.DTO.AdmissionDTO;
using SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.IConfigurationManagmentInternal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Internal.ServicesInternal.ConfigurationManagmentInternal
{
    public class MainSystemServiceInternal : IMainSystemServiceInternal
    {

        #region Batch
        public Batch AddBatch(BatchModel model)
        {
            Batch batch = new Batch()
            {
                IsActive = true,
                CalendarYearId = model.CalendarYearId,
                BranchId = model.BranchId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                CreatedBy = model.createdBy,
                UpdatedBy = model.updatedBy
            };
            return batch;
        }
        public Batch UpdateBatch(BatchModel model)
        {
            Batch batch = new Batch()
            {
                BatchId = model.BatchId,
                IsActive = model.IsActive,
                CalendarYearId = model.CalendarYearId,
                BranchId = model.BranchId,
                UpdatedDate = DateTime.Now,
                UpdatedBy = model.updatedBy
            };
            return batch;
        }
        public List<RegisteredBranchForTheBatch> GetBatchDetailByActiveCalendarYear()
        {
            using(var context = new SCMSEntities())
            {
                List<RegisteredBranchForTheBatch> registeredBranchForTheBatches = new List<RegisteredBranchForTheBatch>();
                var variable = context.sp_GetBranchOnActiveCalendarYear();
                foreach (var v in variable)
                {
                    RegisteredBranchForTheBatch branchs = new RegisteredBranchForTheBatch()
                    {
                        batchId = v.BatchId,
                        branchId = v.BranchId,
                        branchName = v.BranchName,
                        calendarYearEndDate = v.EndDate,
                        calendarYearId = v.CalendarYearId,
                        calendarYearStartDate = v.StartDate,
                        schoolId = v.SchoolID,
                        schoolName = v.SchoolName,
                        isBatchActive = v.IsBatchActive,
                        isBranchActive = v.IsBranchActive,
                        isSchoolActive = v.IsSchoolActive,
                        isMainBranch = v.IsMainBranch
                    };
                    registeredBranchForTheBatches.Add(branchs);
                }
                return registeredBranchForTheBatches;
            }
        }
        public Batch GetBatchesByCalendarYearAndBranch(int calendarYear, int branch)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.Batches.Where(x => x.CalendarYearId == calendarYear && x.BranchId == branch && x.IsActive).FirstOrDefault();
            }
        }
        public Batch GetActiveBatchesByBranch(int branch)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.Batches.Where(x => x.IsActive && x.BranchId == branch).FirstOrDefault();
            }
        }
        public bool isBranchActiveOnBatch(int branchId)
        {
            var branch = GetActiveBatchesByBranch(branchId);
            if (branch == null)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Calander year 
        public CalendarYear AddCalendarYear(CalendarYearModel model)
        {
            CalendarYear cYear = new CalendarYear()
            {
                IsActive = true,
                CalendarYearDescription = model.yearDescription,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                CreatedBy = model.createdBy,
                UpdatedBy = model.updatedBy
            };
            return cYear;
        }
        public CalendarYear UpdateCalendarYear(CalendarYearModel model)
        {
            CalendarYear cYear = new CalendarYear()
            {
                CalendarYearId = model.CalendarYearId,
                IsActive = model.IsActive,
                CalendarYearDescription = model.yearDescription,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                UpdatedDate = DateTime.Now,
                UpdatedBy = model.updatedBy
            };
            return cYear;
        }
        public List<CalendarYear> GetAllCalendarYear()
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.CalendarYears.ToList();
            }

        }
        public CalendarYear GetCalendarYearById(int id)
        {
            using (SCMSEntities context = new SCMSEntities())
                return context.CalendarYears.Find(id);
        }
        public CalendarYear GetActiveCalendarYear()
        {
            using (SCMSEntities context = new SCMSEntities())
                return context.CalendarYears.FirstOrDefault(x => x.IsActive);
        }
        public List<Batch> GetBatchesByCalenderYear(int calendarYear)
        {
            using(var context = new SCMSEntities())
            {
                return context.Batches.Where(x => x.CalendarYearId == calendarYear).ToList();
            }
        }
        #endregion


    }
}
