using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal.Request
{
    public class EnrollStudentRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ClassId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }
        [Required]
        public DateTime EnrolledDate { get; set; }
        public int SectionId { get; set; }
    }
    public class EnrollMultipleStudentRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ClassId { get; set; }
        [Required]
        public List<int> UserId { get; set; }
        [Required]
        public DateTime EnrolledDate { get; set; }
        public int SectionId { get; set; }
    }
}
