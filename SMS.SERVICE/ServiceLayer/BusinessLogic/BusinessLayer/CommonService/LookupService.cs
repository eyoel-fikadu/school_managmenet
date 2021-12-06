using SCMS.DataAccess;
using SCMS.DataAccess.SCMS_Common;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.ICommonService;
using SMS.SERVICE.ServiceLayer.Internal.ServicesInternal.ICommonServiceInternal;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.CommonService
{
    public class LookupService : ILookupService
    {
        private ILookupServiceInternal lookupServiceInternal;

        public LookupService()
        {
            lookupServiceInternal = Singleton.GetLookupServiceInternal();
        }

        #region Namespace
        public List<string> GetNamespaceTypeForMainAdmin()
        {
            return lookupServiceInternal.GetNamespaceType().Where(x => x.IsAdminScoped)
                .Select(x => x.NamespaceTypeId).ToList();
        }

        public List<string> GetNamespaceTypeForSchool()
        {
            return lookupServiceInternal.GetNamespaceType().Where(x => x.IsSchoolScoped)
                .Select(x => x.NamespaceTypeId).ToList();
        }

        public List<string> GetNamespaceTypeForUser()
        {
            return lookupServiceInternal.GetNamespaceType().Where(x => x.IsUserScoped)
                .Select(x => x.NamespaceTypeId).ToList();
        }

        public List<String> GetNamespaceTypeForTables()
        {
            return lookupServiceInternal.GetNamespaceType().Where(x => x.IsTableComponent)
                .Select(x => x.NamespaceTypeId).ToList();
        }
        #endregion

        #region Address Type
        public List<string> GetAddressTypeForUser()
        {
            return lookupServiceInternal.GetAddressType().Where(x => x.IsUserScoped)
                .Select(x => x.AddressTypeId).ToList();
        }

        public List<string> GetAddressTypeForSchool()
        {
            return lookupServiceInternal.GetAddressType().Where(x => x.IsSchoolScoped)
                .Select(x => x.AddressTypeId).ToList();
        }
        #endregion

        #region Lesson
        public List<string> GetDayOfTheWeeks()
        {
            return lookupServiceInternal.GetDayOfTheWeeks().Select(x => x.DayOfTheWeekId).ToList();
        }
        #endregion

        #region Assesment
        public List<string> GetAssesmentTypes()
        {
            return lookupServiceInternal.GetAssesmentTypes().Where(x => x.Catagory == ConstantValues.LOOKUP_VALUE_ASSESMENT_ASSESMENT).Select(x => x.AssesmentTypeId).ToList();
        }

        public List<string> GetExamTypes()
        {
            return lookupServiceInternal.GetAssesmentTypes().Where(x => x.Catagory == ConstantValues.LOOKUP_VALUE_ASSESMENT_EXAM).Select(x => x.AssesmentTypeId).ToList();
        }

        public List<string> GetAssignmentTypes()
        {
            return lookupServiceInternal.GetAssesmentTypes().Where(x => x.Catagory == ConstantValues.LOOKUP_VALUE_ASSESMENT_ASSIGNMENT).Select(x => x.AssesmentTypeId).ToList();
        }

        public List<string> GetEventTypes()
        {
            return lookupServiceInternal.GetEventTypes().Select(x => x.EventTypeId).ToList();
        }
        #endregion

        #region Teacher
        public List<string> GetTeacherTypes()
        {
            return lookupServiceInternal.GetTeacherTypes().Select(x => x.TeacherTypeId).ToList();
        }
        #endregion
    }
}
