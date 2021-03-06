﻿using RealEstate.Entities.Entites;
using RealEstate.Entities.ModelView;
using RealEstate.Service.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service.IService
{
    public interface IRoomService : IBaseService<Room>
    {
        IEnumerable<Room> GetAllListRoom(int top);

        IEnumerable<Room> GetAllListRoomVip(int top, int vipID);

        IEnumerable<Room> GetAllListRoomPaging(SearchRoomEntity filter, int page, int pageSize, out int totalRow);

        IEnumerable<RoomListEntity> GetAllListRoomByUserStoreProc(string userID, string keyword, int page, int pageSize, out int totalRow);

        IEnumerable<Room> GetAllListRoomFullSearch(SearchRoomEntity filter, int page, int pageSize, out int totalRow, string sort);

        IEnumerable<RoomEntity> GetAllListRoomFullSearchStoreProc(SearchRoomEntity filter, int page, int pageSize, out int totalRow, string sort);

        RoomEntity GetRoomByIdStoreProc(int roomId);
        void IncreaseView(int id);

        IEnumerable<RoomEntity> GetReatedRoomByIdStoreProc(int id);

        IEnumerable<RoomListEntity> GetAllRoomHotStoreProc(int top);

        Room InsertRoom(Room room);

        void UpdateRoom(Room room);
    }
}
