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
    using System.Collections.Generic;
    
    public partial class vw_ActiveClassInformation
    {
        public int BatchId { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public bool IsBranchActive { get; set; }
        public bool IsMainBranch { get; set; }
        public int SchoolID { get; set; }
        public string SchoolName { get; set; }
        public bool IsSchoolActive { get; set; }
        public Nullable<int> ClassId { get; set; }
        public string ClassName { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> SectionId { get; set; }
        public string SectionName { get; set; }
        public Nullable<int> SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Nullable<int> TeacherId { get; set; }
        public Nullable<bool> isTeacherActive { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string TeacherTypeId { get; set; }
    }
}
