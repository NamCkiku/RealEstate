using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entities.ModelView
{
    public class SearchRoomEntity
    {
        public string RoomTypeID { get; set; }
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }

        public int? AcreageFrom { get; set; }
        public int? AcreageTo { get; set; }

        public int? WardID { get; set; }

        public int? DistrictID { get; set; }

        public int? ProvinceID { get; set; }
        public string Keywords { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Status { get; set; }
    }
}
