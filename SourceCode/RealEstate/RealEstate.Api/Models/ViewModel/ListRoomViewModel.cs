using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Api.Models.ViewModel
{
    public class ListRoomViewModel
    {
        public int ID { set; get; }

        public string RoomName { set; get; }
        public string Alias { set; get; }

        public int RoomTypeID { set; get; }

        public int? DistrictID { get; set; }

        public int ProvinceID { get; set; }

        public int? PaymentID { get; set; }

        public string ThumbnailImage { set; get; }

        public double? Acreage { get; set; }

        public decimal Price { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }


        public int? ViewCount { set; get; }

        public string MetaKeyword { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTitle { get; set; }

        public string Tags { set; get; }

        public DateTime? CreatedDate { get; set; }

        public string RoomTypeName { get; set; }

        public int RoomStar { get; set; }

        public string ProvinceName { get; set; }
    }
}