using SMS.SERVICE.DTO.AdmissionDTO;
using SMS.SERVICE.DTO.ConfigurationManagmentDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IConfigurationManagment
{
    public interface IMainSystemService
    {

        #region calendar year
        CalendarYearModel CreateCalendarYear(CalendarYearModel calendarYearModel);
        CalendarYearModel UpdateCalendarYear(CalendarYearModel calendarYearModel);
        CalendarYearModel GetActiveCalendarYear();
        CalendarYearModel CloseActiveCalendarYear(DateTime endDate, int updatedBy);
        List<CalendarYearModel> GetAllCalendarYear();
        CalendarYearModel GetCalendarYearById(int id);
        bool IsActiveCalendarYearExist();

        #endregion

        #region Batch
        BatchModel CreateBatch(BatchModel batchModel);
        BatchModel UpdateBatch(BatchModel batchModel);
        int GetActiveBatchIdByBranchId(int branchId);
        List<BatchModel> CreateListOfBatches(List<BatchModel> batchModeles);
        List<BatchModel> DisableBatchesBySchool(int schoolId, int updatedBy);
        BatchModel DisableBatch(int branchId, int updatedBy);
        List<RegisteredBranchForTheBatch> GetBatchDetailByActiveCalendarYear();
        List<BranchModel> GetUnRegisteredBranchesForCalenderYear();
        BatchModel EnableBatch(int branchId, int updatedBy);
        List<BatchModel> EnableBatchesBySchool(int schoolId, int updatedBy);
        #endregion
    }
}
