using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.StudentPortal.Request
{
    public class Student_GetResultRequest
    {
        public int subjectId { get; set; }
        public int assesmentId { get; set; }
    }
}
