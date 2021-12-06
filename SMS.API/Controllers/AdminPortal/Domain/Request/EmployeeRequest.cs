using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.AdminPortal.Request
{
    public class EmployeeRequest
    {
        public int EmployeeId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required]
        [NotNull]
        public String EmployeeType { get; set; }
        public bool IsActive { get; set; }

    }
}
