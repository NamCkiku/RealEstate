using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entities.ModelView
{
    public class AuditlogEntity
    {
        public int AuditLogID { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public int LogType { get; set; }

        public string Description { get; set; }

        public string IPAddress { get; set; }

        public string Device { get; set; }

        public string UserID { get; set; }
    }
}
