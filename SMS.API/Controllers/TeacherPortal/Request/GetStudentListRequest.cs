using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.TeacherPortal.Request
{

    public class GetStudentListRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int classId { get; set; }
        public int sectionId { get; set; }
    }
}
