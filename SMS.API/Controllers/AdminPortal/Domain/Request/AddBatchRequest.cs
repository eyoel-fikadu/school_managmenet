using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.AdminPortal.Domain.Request
{
    public class AddBatchRequest
    {
        [Required]
        public List<int> BranchId { get; set; }
    }
}
