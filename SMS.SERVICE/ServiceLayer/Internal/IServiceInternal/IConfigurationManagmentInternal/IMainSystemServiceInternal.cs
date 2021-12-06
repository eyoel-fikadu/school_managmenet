using SCMS.DataAccess;
using SMS.SERVICE.DTO.AdmissionDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.IConfigurationManagmentInternal
{
    public interface IMainSystemServiceInternal
    {

        #region calendar year
        CalendarYear AddCalendarYear(CalendarYearModel calendarYearModel);
        CalendarYear UpdateCalendarYear(CalendarYearModel calendarYearModel);
        List<CalendarYear> GetAllCalendarYear();
        CalendarYear GetCalendarYearById(int id);
        CalendarYear GetActiveCalendarYear();

        #endregion

        #region Batch
        Batch AddBatch(BatchModel batchModel);
        Batch UpdateBatch(BatchModel batchModel);
        List<RegisteredBranchForTheBatch> GetBatchDetailByActiveCalendarYear();
        Batch GetBatchesByCalendarYearAndBranch(int calendarYear, int branch);
        Batch GetActiveBatchesByBranch(int branch);
        List<Batch> GetBatchesByCalenderYear(int calendarYear);
        bool isBranchActiveOnBatch(int branchId);
        #endregion
    }
}
