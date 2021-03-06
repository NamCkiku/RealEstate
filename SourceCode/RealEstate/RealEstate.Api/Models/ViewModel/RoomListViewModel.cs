﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Api.Models.ViewModel
{
    public class RoomListViewModel
    {
        public int ID { set; get; }

        public string RoomName { set; get; }
        public string Alias { set; get; }

        public int RoomTypeID { set; get; }

        public int? WardID { get; set; }

        public int? DistrictID { get; set; }

        public int ProvinceID { get; set; }

        public int? VipID { get; set; }

        public int? MoreInfomationID { get; set; }

        public int? PaymentID { get; set; }

        public string ThumbnailImage { set; get; }
        public string MoreImages { get; set; }

        public double? Acreage { get; set; }

        public decimal Price { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Sex { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public string UserID { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public double? Lat { get; set; }

        public double? Lng { get; set; }

        public int? DisplayOrder { get; set; }

        public int? ViewCount { set; get; }

        public string MetaKeyword { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTitle { get; set; }

        public string Tags { set; get; }

        public bool Published { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public bool isDelete { get; set; }

        public bool Status { get; set; }

        public string RoomTypeName { get; set; }
        public string FloorNumber { get; set; }

        public string ToiletNumber { get; set; }

        public string BedroomNumber { get; set; }

        public string Compass { get; set; }

        public decimal? ElectricPrice { get; set; }

        public decimal? WaterPrice { get; set; }

        public string Convenient { get; set; }

        public string ProvinceName { get; set; }

        public string Avatar { get; set; }
    }
}