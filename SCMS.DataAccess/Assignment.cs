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
    
    public partial class Assignment
    {
        public int AssignmentId { get; set; }
        public int AssesmentId { get; set; }
        public string AssignmentTypeId { get; set; }
        public System.DateTime AssignedDate { get; set; }
        public System.DateTime SubmissionDate { get; set; }
        public System.TimeSpan SubmissionTIme { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual Assesment Assesment { get; set; }
        public virtual AssesmentType AssesmentType { get; set; }
    }
}