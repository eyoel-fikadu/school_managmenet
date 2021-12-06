using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal.Request
{
    public class GetActiveTimeTableByBranchRequest
    {
        [Required]
        public String DayOfTheWeek { get; set; }
    }
}
