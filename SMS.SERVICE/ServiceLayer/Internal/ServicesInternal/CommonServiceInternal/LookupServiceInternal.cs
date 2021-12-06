using SCMS.DataAccess;
using SMS.SERVICE.ServiceLayer.Internal.ServicesInternal.ICommonServiceInternal;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.SERVICE.ServiceLayer.Internal.ServicesInternal.CommonServiceInternal
{
    public class LookupServiceInternal : ILookupServiceInternal
    {
        public List<AddressType> GetAddressType()
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.AddressTypes.ToList();
            }
        }

        public List<AssesmentType> GetAssesmentTypes()
        {
            using(SCMSEntities context = new SCMSEntities())
            {
                return context.AssesmentTypes.ToList();
            }
        }

        public List<ClassType> GetClassTypes()
        {
            using(SCMSEntities context = new SCMSEntities())
            {
                return context.ClassTypes.ToList();
            }
        }

        public List<DayOfTheWeek> GetDayOfTheWeeks()
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.DayOfTheWeeks.ToList();
            }
        }

        public List<EmployeeType> GetEmployeeTypes()
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.EmployeeTypes.ToList();
            }
        }

        public List<EventType> GetEventTypes()
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.EventTypes.ToList();
            }
        }

        public List<NamespaceType> GetNamespaceType()
        {
            using(SCMSEntities context = new SCMSEntities())
            {
                return context.NamespaceTypes.ToList();
            }
        }

        public List<SubjectType> GetSubjectTypes()
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.SubjectTypes.ToList();
            }
        }

        public List<TeacherType> GetTeacherTypes()
        {
            using (SCMSEntities context = new SCMSEntities())
            {
                return context.TeacherTypes.ToList();
            }
        }
    }
}
