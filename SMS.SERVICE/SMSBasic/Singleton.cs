using SCMS.DataAccess;
using SMS.SERVICE.Service_Layer.IService.IAdmission;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.CommonService;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.SecurityService;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.ICommonService;
using SMS.SERVICE.ServiceLayer.Internal.IClassActivity;
using SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.IClassActivityInternal;
using SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.ICommunicationServiceInternal;
using SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.IConfigurationManagmentInternal;
using SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.IEventManagmentInternal;
using SMS.SERVICE.ServiceLayer.Internal.IServiceInternal.IStudentServiceInternal;
using SMS.SERVICE.ServiceLayer.Internal.ServicesInternal.ClassActivityInternal;
using SMS.SERVICE.ServiceLayer.Internal.ServicesInternal.CommonServiceInternal;
using SMS.SERVICE.ServiceLayer.Internal.ServicesInternal.CommunicationServiceInternal;
using SMS.SERVICE.ServiceLayer.Internal.ServicesInternal.ConfigurationManagmentInternal;
using SMS.SERVICE.ServiceLayer.Internal.ServicesInternal.EventManagementInternal;
using SMS.SERVICE.ServiceLayer.Internal.ServicesInternal.ICommonServiceInternal;
using SMS.SERVICE.ServiceLayer.Internal.ServicesInternal.StudentServiceInternal;
using SMS.SERVICE.ServiceLayer.IService.IAdmission;
using SMS.SERVICE.ServiceLayer.IService.ICommonService;
using SMS.SERVICE.ServiceLayer.IService.IConfigurationManagment;
using SMS.SERVICE.ServiceLayer.Services.CommonService;
using SMS.SERVICE.Services;
using SMS.SERVICE.Services.Admission;
using SMS.SERVICE.Services.ConfigurationManagment;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.SMSBasic
{
    public class Singleton
    {
        private static SCMSEntities _context;
        
        private static IEnrollmentServiceInternal enrollmentServiceInternal;
        private static IUserServiceInternal userServiceInternal;
        private static IAddressServiceInternal addressServiceInternal;
        private static ISchoolServiceInternal schoolServiceInternal;
        private static IAddressService addressService;
        private static ILookupServiceInternal lookupServiceInternal;
        private static IPhoneNumberServiceInternal phoneNumberServiceInternal;
        private static ICuriculumServiceInternal curiculumServiceInternal;
        private static IMainSystemServiceInternal mainSystemServiceInternal;
        private static ITimeTableServiceInternal timeTableServiceInternal;
        private static IAttendanceServiceInternal attendanceServiceInternal;
        private static ICalendarServiceInternal calendarServiceInternal;
        private static IAssesmentServiceInternal assesmentServiceInternal;
        private static JwtSettings jwt;

        public static SCMSEntities GetSCMSEntities()
        {
            if (_context == null)
            {
                _context = new SCMSEntities();
            }
            return _context;
        }
        
        public static IEnrollmentServiceInternal GetEnrollmentServiceInternal()
        {
            if(enrollmentServiceInternal == null)
            {
                enrollmentServiceInternal = new EnrollmentServiceInternal();
            }
            return enrollmentServiceInternal;
        }

        public static IUserServiceInternal GetUserServiceInternal()
        {
            if (userServiceInternal == null)
            {
                userServiceInternal = new UserServiceInternal();
            }
            return userServiceInternal;
        }

        public static IAddressServiceInternal GetAddressServiceInternal()
        {
            if(addressServiceInternal == null)
            {
                addressServiceInternal = new AddressServiceInternal();
            }
            return addressServiceInternal;
        }

        public static IAddressService GetAddressService()
        {
            if(addressService == null)
            {
                addressService = new AddressService();
            }
            return addressService;
        }

        public static ISchoolServiceInternal GetSchoolServiceInternal()
        {
            if(schoolServiceInternal == null)
            {
                schoolServiceInternal = new SchoolServiceInternal();
            }
            return schoolServiceInternal;
        }

        public static ILookupServiceInternal GetLookupServiceInternal()
        {
            if(lookupServiceInternal == null)
            {
                lookupServiceInternal = new LookupServiceInternal();
            }
            return lookupServiceInternal;
        }

        public static IPhoneNumberServiceInternal GetPhoneNumberServiceInternal()
        {
            if(phoneNumberServiceInternal == null)
            {
                phoneNumberServiceInternal = new EthioTelecomPhoneNumberService();
            }
            return phoneNumberServiceInternal;
        }

        public static ICuriculumServiceInternal GetCuriculumServiceInternal()
        {
            if (curiculumServiceInternal == null)
            {
                curiculumServiceInternal = new CuriculumServiceInternal();
            }
            return curiculumServiceInternal;
        }

        public static IMainSystemServiceInternal GetMainSystemServiceInternal()
        {
            if(mainSystemServiceInternal == null)
            {
                mainSystemServiceInternal = new MainSystemServiceInternal();
            }
            return mainSystemServiceInternal;
        }

        public static ITimeTableServiceInternal GetTimeTableServiceInternal()
        {
            if(timeTableServiceInternal == null)
            {
                timeTableServiceInternal = new TimeTableServiceInternal();
            }
            return timeTableServiceInternal;
        }
        public static IAttendanceServiceInternal GetAttendanceServiceInternal()
        {
            if (attendanceServiceInternal == null)
            {
                attendanceServiceInternal = new AttendanceServiceInternal();
            }
            return attendanceServiceInternal;
        }

        internal static ICalendarServiceInternal GetCalanderServiceInternal()
        {
            if(calendarServiceInternal == null)
            {
                calendarServiceInternal = new CalendarServiceInternal();
            }
            return calendarServiceInternal;
        }

        internal static IAssesmentServiceInternal GetAssesmentServiceInternal()
        {
            if(assesmentServiceInternal == null)
            {
                assesmentServiceInternal = new AssesmentServiceInternal();
            }
            return assesmentServiceInternal;
        }

        internal static JwtSettings GetJwtSettings()
        {
            if(jwt == null)
            {
                jwt = new JwtSettings();
            }
            return jwt; 
        }
    }
}
