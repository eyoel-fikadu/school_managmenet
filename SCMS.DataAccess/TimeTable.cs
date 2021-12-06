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
    
    public partial class TimeTable
    {
        public int TimeTableId { get; set; }
        public string Description { get; set; }
        public int BatchId { get; set; }
        public int BranchId { get; set; }
        public string DayOfTheWeekId { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual Batch Batch { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual DayOfTheWeek DayOfTheWeek { get; set; }
    }
}