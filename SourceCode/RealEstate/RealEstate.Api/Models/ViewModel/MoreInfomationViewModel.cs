using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Api.Models.ViewModel
{
    public class MoreInfomationViewModel
    {
        public int MoreInfomationID { get; set; }

        public string FloorNumber { get; set; }

        public string ToiletNumber { get; set; }

        public string BedroomNumber { get; set; }

        public string Compass { get; set; }

        public int? ElectricPrice { get; set; }

        public int? WaterPrice { get; set; }

        public string Convenient { get; set; }
    }
}