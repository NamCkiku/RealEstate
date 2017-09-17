using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Api.Models.ViewModel
{
    public class AuditlogViewModel
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