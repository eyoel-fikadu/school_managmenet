using SCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Internal.ServicesInternal.ICommonServiceInternal
{
    public interface ILookupServiceInternal
    {
        List<NamespaceType> GetNamespaceType();
        List<AddressType> GetAddressType();
        List<TeacherType> GetTeacherTypes();
        List<SubjectType> GetSubjectTypes();
        List<EmployeeType> GetEmployeeTypes();
        List<DayOfTheWeek> GetDayOfTheWeeks();
        List<ClassType> GetClassTypes();
        List<EventType> GetEventTypes();
        List<AssesmentType> GetAssesmentTypes();
    }
}
