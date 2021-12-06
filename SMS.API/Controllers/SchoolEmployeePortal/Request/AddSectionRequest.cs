using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal.Request
{
    public class AddSectionRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ClassId { get; set; }
        [Required]
        [NotNull]
        public String SectionName { get; set; }
        
    }
    public class AddSectionListRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ClassId { get; set; }
        [Required]
        [NotNull]
        public List<String> SectionName { get; set; }

    }
}
