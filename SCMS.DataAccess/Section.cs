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
    
    public partial class Section
    {
        public int SectionId { get; set; }
        public int ClassId { get; set; }
        public string SectionName { get; set; }
        public Nullable<int> AssignedRoom { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> CreatedUser { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedUser { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual Class Class { get; set; }
    }
}
