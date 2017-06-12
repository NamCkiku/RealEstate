using RealEstate.Administrator.Models.ViewModel;
using RealEstate.Entities.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Administrator.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdateRoomType(this RoomType roomType, RoomTypeViewModel roomTypeVm)
        {
            roomType.ID = roomTypeVm.ID;
            roomType.RoomTypeName = roomTypeVm.RoomTypeName;
            roomType.Description = roomTypeVm.Description;
            roomType.Alias = roomTypeVm.Alias;
            roomType.ParentID = roomTypeVm.ParentID;
            roomType.DisplayOrder = roomTypeVm.DisplayOrder;
            roomType.ImageIcon = roomTypeVm.ImageIcon;
            roomType.HomeFlag = roomTypeVm.HomeFlag;
            roomType.CreatedDate = roomTypeVm.CreatedDate;
            roomType.CreatedBy = roomTypeVm.CreatedBy;
            roomType.UpdatedDate = roomTypeVm.UpdatedDate;
            roomType.UpdatedBy = roomTypeVm.UpdatedBy;
            roomType.MetaKeyword = roomTypeVm.MetaKeyword;
            roomType.MetaDescription = roomTypeVm.MetaDescription;
            roomType.Status = roomTypeVm.Status;
            roomType.isDelete = false;
        }
    }
}