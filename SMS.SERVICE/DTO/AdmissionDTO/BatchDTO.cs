using com.sun.istack.@internal;
using JetBrains.Annotations;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SMS.SERVICE.DTO.AdmissionDTO
{
    public class BatchModel : AddTableModel
    {
        public int BatchId { get; set; }

        [Required]
        [Range(1,int.MaxValue)]
        public int CalendarYearId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int BranchId { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
