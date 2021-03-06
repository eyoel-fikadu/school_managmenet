using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.SERVICE.SMSBasic
{
    public enum CustomResponse
    {
        //Generic
        ERROR_RESPONSE_GENERIC = -1,
        ERROR_LIST_OF_FAILED_ERRORS = -2,
        SUCCESS_RESPONSE = 1,
        UNABLE_TO_ACCESS_RESOURCE = -3,
        UNAUTHORIZED_USER_TRIES_TO_ACCESS = -4,

        //User response
        ERROR_RESPONSE_USER_DOESNT_EXIST = 1001,
        ERROR_RESPONSE_USER_IS_NOT_EMPLOYEE = 1002,
        ERROR_RESPONSE_USER_IS_NOT_TEACHER = 1003,
        ERROR_RESPONSE_USER_IS_NOT_IT_STAFF = 1004,
        ERROR_RESPONSE_USER_IS_NOT_STUDENT = 1005,
        ERROR_RESPONSE_USER_IS_NOT_TD_WEB_ADMIN = 1006,
        USER_NAME_AND_PASSWORD_DOESNT_EXIST = 1009,

        //Employee Response
        EMPLOYEE_ALREADY_REGISTERED = 1101,
        EMPLOYEE_IS_NOT_A_MEMBER_OF_A_BRANCH = 1102,
        EMPLOYEE_IS_NOT_TEACHER = 1103,
        EMPLOYEE_IS_NOT_ACTIVE = 1104,
        EMPLOYEE_IS_NOT_ADDED_TO_CURRENT_CALENDAR_YEAR = 1105,

        //Student Exceptions
        STUDENT_IS_NOT_PART_OF_A_CLASS = 1201,
        STUDENT_ALREADY_REGISTERED = 1202,
        STUDENT_DOESNT_EXIST = 1203,
        STUDENT_ID_SHOULD_BE_IDENTICAL = 1204,

        //School security response
        ERROR_RESPONSE_UNABLE_TO_LOAD_BRANCH = 2001,
        BRANCH_SHOULD_BE_MAIN_BRANCH = 2002,
        BRANCH_NOT_ACCESSABLE_BY_SCHOOL = 2003,
        BRANCH_IS_NOT_ACTIVE = 2004,
        BRANCH_ALREADY_REGISTERED = 2005,
        BRANCH_ALREADY_ADDED_TO_CALENDAR_YEAR = 2006,
        BRANCH_IS_NOT_ADDED_TO_CALENDAR_YEAR = 2007,
        SCHOOL_IS_NOT_ACTIVE = 2008,
        BRANCH_DOESNT_EXIST = 2009,
        SCHOOL_DOESNT_EXIST = 2010,
        MAIN_BRANCH_ALREADY_EXISTS = 2011,

        //Namespace response
        NAMESPACE_NOT_PROVIDED = 3001,
        NAMESPACE_NOT_ALLOWED = 3002,
        EMPLOYEE_TYPE_NOT_PROVIDED = 3003,

        //Class Response
        UNABLE_TO_ACCESS_CLASS_INFORMATION = 4001,
        UNABLE_TO_ACCESS_SECTION_INFORMATION = 4002,
        SECTION_SHOULD_BE_PROVIDED = 4003,
        CLASS_IS_SECTION_LESS = 4004,
        CLASS_ALREADY_REGISTERED = 4005,
        SECTION_ALREADY_REGISTERED = 4006,
        CLASS_IS_NOT_ACCESSED_BY_EMPLOYEE = 4007,
        SECTION_IS_NOT_ACCESSED_BY_TEACHER = 4008,
        TEACHER_CANNOT_ACCESS_RESOURCE = 4009,
        CLASS_INFORMATION_IS_NOT_PROVIDED = 4010,
        SUBJECT_IS_NOT_ACCESSED_BY_EMPLOYEE = 4011,

        //Schedule Response
        SCHEDULE_DOESNT_EXIST = 4101,
        SCHEDULE_DETAIL_SHOULD_BE_IDENTICAL = 4102,
        SCHEDULE_DETAIL_PER_SCHEDULE_ALREADY_ADDED = 4103,
        SCHEDULE_NOT_ACCESSABLE_BY_TEACHER = 4104,

        //Assesment Response
        ASSESMENT_SHOULD_BE_IDENTICAL = 4201,
        ASSESMENT_DOESNT_EXIST = 4202,

        //Add New Exceptions
        USER_NAME_ALREADY_EXISTS = 5001,
        PHONE_NUMBER_ALREADY_EXISTS = 5002,
        EMAIL_ALREADY_EXISTS = 5003,
        PHONE_NUMBER_FORMAT_ERROR = 5004,
        PHONE_NUMBER_NOT_SUPPORTED = 5005,
        ACTIVE_CALENDAR_YEAR_EXIST = 5008,
        SCHOOL_NAME_EXIST = 5009,
        SCHOOL_EMAIL_EXIST = 5010,
        SCHOOL_TINNUMBER_EXIST = 5011,
        SCHOOL_WEBSITE_EXIST = 5012,
        SCHDEULE_ALREADY_EXISTS = 5013,
        TEACHER_ALREADY_ASSIGNED = 5014,
        SUBJECT_ALREADY_EXISTS = 5017,

        //TD-WEB Managment Exception
        ACTIVE_CALENDAR_YEAR_DOESNT_EXIST = 7001,

        
    }
}
