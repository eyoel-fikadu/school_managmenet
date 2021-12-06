using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.AdminPortal.Domain.Request
{
    public class Admin_AddEmployeeRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int BranchId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
    }
}
