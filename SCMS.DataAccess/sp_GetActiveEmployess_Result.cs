//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCMS.DataAccess
{
    using System;
    
    public partial class sp_GetActiveEmployess_Result
    {
        public int UserID { get; set; }
        public bool PhoneNumberVerified { get; set; }
        public bool EmailVerified { get; set; }
        public string NameSpace { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsLocked { get; set; }
        public bool IsActive { get; set; }
        public string PublicId { get; set; }
        public int EmployeeId { get; set; }
        public bool isEmployeeActive { get; set; }
        public int SchoolID { get; set; }
        public bool IsSchoolActive { get; set; }
        public int BranchId { get; set; }
        public bool IsBranchActive { get; set; }
        public bool IsMainBranch { get; set; }
        public int BatchId { get; set; }
        public bool IsBatchActive { get; set; }
        public int CalendarYearId { get; set; }
        public bool IsCalendarYearActive { get; set; }
        public string EmployeeTypeId { get; set; }
    }
}
