using SCMS.DataAccess;
using SMS.SERVICE.DTO;
using SMS.SERVICE.DTO.AdmissionDTO;
using SMS.SERVICE.DTO.ConfigurationManagmentDTO;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IConfigurationManagment;
using SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.IConfigurationManagmentInternal;
using SMS.SERVICE.ServiceLayer.IService.IConfigurationManagment;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.ConfigurationManagment
{
    public class MainSystemService : IMainSystemService
    {
        private IMainSystemServiceInternal mainSystemServiceInternal;
        private ISchoolServiceInternal schoolServiceInternal;
        public MainSystemService()
        {
            mainSystemServiceInternal = Singleton.GetMainSystemServiceInternal();
            schoolServiceInternal = Singleton.GetSchoolServiceInternal();
        }

        #region batch
        public BatchModel CreateBatch(BatchModel model)
        {
            ValidateBatch(model);
            using (SCMSEntities context = new SCMSEntities())
            {
                Batch batch = mainSystemServiceInternal.AddBatch(model);
                context.Batches.Add(batch);
                context.SaveChanges();
                model.BatchId = batch.BatchId;
                return model;
            }
        }
        private void ValidateBatch(BatchModel model)
        {
            Batch batch = mainSystemServiceInternal.GetBatchesByCalendarYearAndBranch(model.CalendarYearId, model.BranchId);
            if (batch != null)
            {
                throw CommonMethods.GetException(CustomResponse.BRANCH_ALREADY_REGISTERED);
            }
        }
        public BatchModel UpdateBatch(BatchModel model)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                Batch batch = mainSystemServiceInternal.UpdateBatch(model);
                context.Batches.AddOrUpdate(batch);
                context.SaveChanges();
                return model;
            }
        }
        public List<BatchModel> CreateListOfBatches(List<BatchModel> models)
        {
            var activeBatches = mainSystemServiceInternal.GetBatchDetailByActiveCalendarYear();
            var branches = schoolServiceInternal.GetAllActiveBranches().Select(x => x.BranchId).ToList();
            
            using (SCMSEntities context = new SCMSEntities())
            {
                foreach (BatchModel model in models)
                {
                    if(!branches.Contains(model.BranchId))
                    {
                        throw CommonMethods.GetException(CustomResponse.BRANCH_IS_NOT_ACTIVE);
                    }
                    if(activeBatches.FirstOrDefault(x => x.branchId == model.BranchId) != null)
                    {
                        throw CommonMethods.GetException(CustomResponse.BRANCH_ALREADY_ADDED_TO_CALENDAR_YEAR );
                    }
                    Batch batch = mainSystemServiceInternal.AddBatch(model);
                    context.Batches.Add(batch);
                }
                context.SaveChanges();
                return models;
            }
        }
        public List<RegisteredBranchForTheBatch> GetBatchDetailByActiveCalendarYear()
        {
            return mainSystemServiceInternal.GetBatchDetailByActiveCalendarYear();
        }
        public BatchModel DisableBatch(int branchId, int updatedBy)
        {
            var disabledBbatch = mainSystemServiceInternal.GetBatchDetailByActiveCalendarYear().FirstOrDefault(x => x.branchId == branchId);
            if(disabledBbatch != null)
            {
                using(var context = new SCMSEntities())
                {
                    BatchModel model = new BatchModel()
                    {
                        BatchId = disabledBbatch.batchId,
                        BranchId = disabledBbatch.branchId,
                        CalendarYearId = disabledBbatch.calendarYearId,
                        IsActive = false,
                        updatedBy = updatedBy
                    };
                    Batch batch = mainSystemServiceInternal.UpdateBatch(model);
                    context.Batches.AddOrUpdate(batch);
                    context.SaveChanges();
                    return model;
                }
            }
            else
            {
                throw CommonMethods.GetException(CustomResponse.BRANCH_IS_NOT_ADDED_TO_CALENDAR_YEAR);
            }
        }
        public List<BatchModel> DisableBatchesBySchool(int schoolId, int updatedBy)
        {
            var disabledSchool = mainSystemServiceInternal.GetBatchDetailByActiveCalendarYear().Where(x => x.schoolId == schoolId).ToList();
            if (disabledSchool != null && disabledSchool.Count > 0)
            {
                using (var context = new SCMSEntities())
                {
                    List<BatchModel> models = new List<BatchModel>(); 
                    foreach (var disabledBatch in disabledSchool)
                    {
                        BatchModel model = new BatchModel()
                        {
                            BatchId = disabledBatch.batchId,
                            BranchId = disabledBatch.branchId,
                            CalendarYearId = disabledBatch.calendarYearId,
                            IsActive = false,
                            updatedBy = updatedBy
                        };
                        Batch batch = mainSystemServiceInternal.UpdateBatch(model);
                        context.Batches.AddOrUpdate(batch);
                        models.Add(model);
                    }
                    context.SaveChanges();
                    return models;
                }
            }
            else
            {
                throw CommonMethods.GetException(CustomResponse.SCHOOL_IS_NOT_ACTIVE);
            }
        }
        public BatchModel EnableBatch(int branchId, int updatedBy)
        {
            var disabledBbatch = mainSystemServiceInternal.GetBatchDetailByActiveCalendarYear().FirstOrDefault(x => x.branchId == branchId);
            if (disabledBbatch != null)
            {
                if(!disabledBbatch.isBatchActive)
                {
                    using (var context = new SCMSEntities())
                    {
                        BatchModel model = new BatchModel()
                        {
                            BatchId = disabledBbatch.batchId,
                            BranchId = disabledBbatch.branchId,
                            CalendarYearId = disabledBbatch.calendarYearId,
                            IsActive = true,
                            updatedBy = updatedBy
                        };
                        Batch batch = mainSystemServiceInternal.UpdateBatch(model);
                        context.Batches.AddOrUpdate(batch);
                        context.SaveChanges();
                        return model;
                    }
                }
                else
                {
                    throw CommonMethods.GetException(CustomResponse.BRANCH_ALREADY_ADDED_TO_CALENDAR_YEAR );
                }
            }
            else
            {
                throw CommonMethods.GetException(CustomResponse.BRANCH_IS_NOT_ADDED_TO_CALENDAR_YEAR);
            }
        }
        public List<BatchModel> EnableBatchesBySchool(int schoolId, int updatedBy)
        {
            var disabledSchool = mainSystemServiceInternal.GetBatchDetailByActiveCalendarYear().Where(x => x.schoolId == schoolId).ToList();
            if (disabledSchool != null && disabledSchool.Count > 0)
            {
                using (var context = new SCMSEntities())
                {
                    List<BatchModel> models = new List<BatchModel>();
                    foreach (var disabledBatch in disabledSchool)
                    {
                        BatchModel model = new BatchModel()
                        {
                            BatchId = disabledBatch.batchId,
                            BranchId = disabledBatch.branchId,
                            CalendarYearId = disabledBatch.calendarYearId,
                            IsActive = true,
                            updatedBy = updatedBy
                        };
                        Batch batch = mainSystemServiceInternal.UpdateBatch(model);
                        context.Batches.AddOrUpdate(batch);
                        models.Add(model);
                    }
                    context.SaveChanges();
                    return models;
                }
            }
            else
            {
                throw CommonMethods.GetException(CustomResponse.BRANCH_IS_NOT_ADDED_TO_CALENDAR_YEAR);
            }
        }
        public int GetActiveBatchIdByBranchId(int branchId)
        {
            Batch batch = mainSystemServiceInternal.GetActiveBatchesByBranch(branchId);
            if (batch == null)
            {
                throw CommonMethods.GetException(CustomResponse.BRANCH_IS_NOT_ADDED_TO_CALENDAR_YEAR);
            }
            return batch.BatchId;
        }

        #endregion

        #region Calendar Year
        public CalendarYearModel CreateCalendarYear(CalendarYearModel calendarYearModel)
        {
            if (IsActiveCalendarYearExist())
            {
                throw CommonMethods.GetException(CustomResponse.ACTIVE_CALENDAR_YEAR_EXIST);
            }
            using (SCMSEntities context = new SCMSEntities())
            {
                CalendarYear calendarYear = mainSystemServiceInternal.AddCalendarYear(calendarYearModel);
                context.CalendarYears.Add(calendarYear);
                context.SaveChanges();
                calendarYearModel.CalendarYearId = calendarYear.CalendarYearId;
                return calendarYearModel;
            }
        }
        public CalendarYearModel UpdateCalendarYear(CalendarYearModel calendarYearModel)
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                CalendarYear calendarYear = mainSystemServiceInternal.UpdateCalendarYear(calendarYearModel);
                context.CalendarYears.AddOrUpdate(calendarYear);
                context.SaveChanges();
                return calendarYearModel;
            }
        }
        public List<CalendarYearModel> GetAllCalendarYear()
        {
            List<CalendarYear> calendarYears = mainSystemServiceInternal.GetAllCalendarYear();
            List<CalendarYearModel> calendarYearModels = new List<CalendarYearModel>();
            foreach (CalendarYear calendarYear in calendarYears)
            {
                CalendarYearModel calendarYearModel = GetDtoModels.GetCalendarYear(calendarYear);
                calendarYearModels.Add(calendarYearModel);
            }
            return calendarYearModels;
        }
        public CalendarYearModel GetCalendarYearById(int id)
        {
            return GetDtoModels.GetCalendarYear(mainSystemServiceInternal.GetCalendarYearById(id));
        }
        public bool IsActiveCalendarYearExist()
        {
            CalendarYear calendarYear = mainSystemServiceInternal.GetActiveCalendarYear(); 
            if (calendarYear == null) return false;
            return true;
        }
        public CalendarYearModel GetActiveCalendarYear()
        {
            CalendarYear calendarYear = mainSystemServiceInternal.GetActiveCalendarYear();
            if (calendarYear == null) return null;
            return GetDtoModels.GetCalendarYear(calendarYear);
        }
        public CalendarYearModel CloseActiveCalendarYear(DateTime endDate, int updatedBy)
        {
            using(var context = new SCMSEntities())
            {
                CalendarYear calendarYear = mainSystemServiceInternal.GetActiveCalendarYear();

                if (calendarYear != null)
                {
                    CalendarYearModel model = new CalendarYearModel()
                    {
                        CalendarYearId = calendarYear.CalendarYearId,
                        EndDate = endDate,
                        IsActive = false,
                        StartDate = calendarYear.StartDate,
                        yearDescription = calendarYear.CalendarYearDescription
                    };
                    calendarYear = mainSystemServiceInternal.UpdateCalendarYear(model);
                    context.CalendarYears.AddOrUpdate(calendarYear);
                    List<Batch> batches = mainSystemServiceInternal.GetBatchesByCalenderYear(calendarYear.CalendarYearId);
                    foreach(var batch in batches)
                    {
                        batch.IsActive = false;
                        context.Batches.AddOrUpdate(batch);
                    }
                    context.SaveChanges();
                    return model;
                }
                else
                {
                    throw CommonMethods.GetException(CustomResponse.ACTIVE_CALENDAR_YEAR_DOESNT_EXIST);
                }
            }
        }
        public List<BranchModel> GetUnRegisteredBranchesForCalenderYear()
        {
            List<int> registeredBranch = mainSystemServiceInternal.GetBatchDetailByActiveCalendarYear().Select(x => x.branchId).ToList();
            List<Branch> allBranches = schoolServiceInternal.GetAllActiveBranches();
            List<BranchModel> branchModels = new List<BranchModel>();
            foreach(var branch in allBranches)
            {
                if(!registeredBranch.Contains(branch.BranchId))
                {
                    branchModels.Add(GetDtoModels.GetBranchModel(branch));
                }
            }
            return branchModels;
        }
        #endregion
    }
}
