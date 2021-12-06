using JetBrains.Annotations;
using SMS.SERVICE.SMSBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SMS.SERVICE.DTO.AdmissionDTO
{
    public class EmployeeModel : AddTableModel
    {
        public int EmployeeId { get; set; }
        public int UserId { get; set; }
        public int BatchId { get; set; }
        public int BranchId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String EmployeeType { get; set; }
        public bool IsActive { get; set; }

    }

}
