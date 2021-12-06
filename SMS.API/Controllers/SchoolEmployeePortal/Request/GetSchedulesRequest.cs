using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API.WEB.Controllers.SchoolEmployeePortal.Request
{
    public class GetSchedulesRequest
    {
        public int classId { get; set; }
        public int sectionId { get; set; }
    }
}
