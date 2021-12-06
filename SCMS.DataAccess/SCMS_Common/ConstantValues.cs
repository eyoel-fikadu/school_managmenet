using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMS.DataAccess.SCMS_Common
{
    public class ConstantValues
    {
        #region TableName
        public static String TABLE_USER = "USER";
        public static String TABLE_BRANCH = "BRANCH";
        public static String TABLE_SCHOOL = "SCHOOL";

        #endregion

        #region Lookup 
       
        #region Namespace Values
        public const String LOOKUP_VALUE_NAMESPACE_STUDENT = "STUDENT";
        public const String LOOKUP_VALUE_NAMESPACE_TDWEB_ADMIN = "TDWEB_ADMIN";
        public const String LOOKUP_VALUE_NAMESPACE_TEACHER = "TEACHER";
        public const String LOOKUP_VALUE_NAMESPACE_SCHOOL_ADMIN = "SCHOOL_ADMIN";
        public const String LOOKUP_VALUE_NAMESPACE_SCHOOL_MANAGEMENT = "SCHOOL_MANAGEMENT";
        public const String LOOKUP_VALUE_NAMESPACE_PARENT = "PARENT";
        public const String LOOKUP_VALUE_NAMESPACE_SCHOOL_EMPLOYEE = "SCHOOL_EMPLOYEE";
        public const String LOOKUP_VALUE_NAMESPACE_IT_STAFF = "ITSTAFF";
        #endregion

        #region Assesment Values
        public static String LOOKUP_VALUE_ASSESMENT_ASSESMENT = "ASSESMENT";
        public static String LOOKUP_VALUE_ASSESMENT_ASSIGNMENT = "ASSIGNMENT";
        public static String LOOKUP_VALUE_ASSESMENT_EXAM = "EXAM";
        #endregion
        #endregion

        #region Links
        public const string TD_WEB_ADMIN_LINK = "admin";
        public const string STUDENT_LINK = "student";
        public const string TEACHER_LINK = "teacher";
        public const string PARENT_LINK = "parent";
        public const string EMPLOYEE_LINK = "employee";
        #endregion
    }
}
