using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal.Request
{
    public class AssignTeacherRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int SubjectId { get; set; }
        [Required]
        [NotNull]
        public String TeacherTypeId { get; set; }
        [Required]
        [NotNull]
        public DateTime StartDate { get; set; }
        public bool IsActive { get; set; }
    }
}
