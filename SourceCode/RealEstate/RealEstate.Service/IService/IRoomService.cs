using RealEstate.Entities.Entites;
using RealEstate.Entities.ModelView;
using RealEstate.Service.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service.IService
{
    public interface IRoomService : IService<Room>
    {
        IEnumerable<Room> GetAllListRoom(int top);

        IEnumerable<Room> GetAllListRoomVip(int top, int vipID);

        IEnumerable<Room> GetAllListRoomPaging(SearchRoomEntity filter, int page, int pageSize, out int totalRow);

        IEnumerable<Room> GetAllListRoomByUser(string userID, int page, int pageSize, out int totalRow);

        IEnumerable<Room> GetAllListRoomFullSearch(SearchRoomEntity filter, int page, int pageSize, out int totalRow, string sort);

        Room InsertRoom(Room room);

        void UpdateRoom(Room room);
    }
}
