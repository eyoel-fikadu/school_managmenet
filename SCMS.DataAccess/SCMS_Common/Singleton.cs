using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMS.DataAccess.SCMS_Common
{
    public class Singleton
    {
        private static SCMSEntities _context;
        public static SCMSEntities GetSCMSEntities()
        {
            if (_context == null) return new SCMSEntities();
            return _context;
        }
    }
}
