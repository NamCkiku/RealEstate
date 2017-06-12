using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entities.ModelView
{
    public class SearchRoomTypeEntity
    {
        public string Keywords { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Status { get; set; }
    }
}
