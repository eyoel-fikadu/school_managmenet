using SCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.ICommonService
{
    public interface ILookupService
    {
        #region Namespace
        List<String> GetNamespaceTypeForUser();
        List<String> GetNamespaceTypeForSchool();
        List<String> GetNamespaceTypeForMainAdmin();
        List<String> GetNamespaceTypeForTables();
        #endregion

        #region Adress Type
        List<String> GetAddressTypeForUser();
        List<String> GetAddressTypeForSchool();
        #endregion

        #region Lesson 
        List<String> GetDayOfTheWeeks();
        #endregion

        #region Assesment
        List<String> GetAssesmentTypes();
        List<String> GetExamTypes();
        List<String> GetAssignmentTypes();

        #endregion

        #region Event
        List<String> GetEventTypes();

        #endregion

        #region Employee
        List<String> GetTeacherTypes();
        #endregion
    }
}
