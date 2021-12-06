using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal.Request
{
    public class AssignSectionRequest
    {
        [Required]
        [Range(1,int.MaxValue)]
        public int StudentId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int SectionId { get; set; }
    }
    public class AssignMultipleSectionRequest
    {
        [Required]
        public List<int> StudentId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int SectionId { get; set; }
    }
}
