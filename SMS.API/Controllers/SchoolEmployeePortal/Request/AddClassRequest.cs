using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal.Request
{
    public class AddClassRequest
    {

        [Required]
        [NotNull]
        public String ClassName { get; set; }
        [Required]
        [NotNull]
        public bool HasSection { get; set; }
    }
    public class AddClassListRequest
    {

        [Required]
        [NotNull]
        public List<AddClassRequest> classes{ get; set; }
    }
}
