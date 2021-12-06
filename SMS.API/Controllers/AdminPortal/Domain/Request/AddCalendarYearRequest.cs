using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.AdminPortal.Domain.Request
{
    public class AddCalendarYearRequest
    {
        public String yearDescription { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
    public class CloseCalendarYearRequest
    {
        [Required]
        public DateTime EndDate { get; set; }
    }
}
