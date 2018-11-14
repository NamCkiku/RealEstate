using RealEstate.Api.Models.ViewModel;
using RealEstate.Entities.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Api.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdateRoom(this Room room, RoomViewModel roomVM)
        {
            room.ID = roomVM.ID;
            room.RoomName = roomVM.RoomName;
            room.Description = roomVM.Description;
            room.Alias = roomVM.Alias;
            room.Lat = roomVM.Lat;
            room.Lng = roomVM.Lng;
            room.PaymentID = roomVM.PaymentID;
            room.RoomTypeID = roomVM.RoomTypeID;
            room.VipID = roomVM.VipID;
            room.MoreInfomationID = roomVM.MoreInfomationID;
            room.ProvinceID = roomVM.ProvinceID;
            room.DistrictID = roomVM.DistrictID;
            room.WardID = roomVM.WardID;
            room.UserID = roomVM.UserID;
            room.Price = roomVM.Price;
            room.Acreage = roomVM.Acreage;
            room.Email = roomVM.Email;
            room.Sex = roomVM.Sex;
            room.Tags = roomVM.Tags;
            room.Published = roomVM.Published;
            room.UserName = roomVM.UserName;
            room.Address = roomVM.Address;
            room.Phone = roomVM.Phone;
            room.Content = roomVM.Content;
            room.IsDelete = false;
            room.IsActive = false;
            room.Address = roomVM.Address;
            room.DisplayOrder = roomVM.DisplayOrder;
            room.ThumbnailImage = roomVM.ThumbnailImage;
            room.MoreImages = roomVM.MoreImages;
            room.CreatedDate = DateTime.Now;
            room.CreatedBy = roomVM.CreatedBy;
            room.UpdatedDate = roomVM.UpdatedDate;
            room.UpdatedBy = roomVM.UpdatedBy;
            room.MetaKeyword = roomVM.MetaKeyword;
            room.MetaDescription = roomVM.MetaDescription;
            room.MetaTitle = roomVM.MetaTitle;
            room.Status = roomVM.Status;
            room.ViewCount = roomVM.ViewCount;
        }
    }
}