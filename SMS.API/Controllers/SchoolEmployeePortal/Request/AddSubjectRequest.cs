using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal.Request
{
    public class AddSubjectRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ClassID { get; set; }
        [Required]
        [NotNull]
        public String SubjectName { get; set; }
    }
    public class AddSubjectListRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ClassID { get; set; }
        [Required]
        [NotNull]
        public List<String> SubjectName { get; set; }
    }
}
