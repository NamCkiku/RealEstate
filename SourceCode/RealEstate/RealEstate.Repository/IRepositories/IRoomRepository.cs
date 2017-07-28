using RealEstate.Entities.Entites;
using RealEstate.Entities.ModelView;
using RealEstate.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Repository.IRepositories
{
    public interface IRoomRepository : IRepository<Room>
    {
        IEnumerable<RoomEntity> GetAllRoom();
        IEnumerable<RoomEntity> GetAllRoomPagingFullSearch(SearchRoomEntity filter, int page, int pageSize, out int totalRow, string sort);
        IEnumerable<RoomEntity> GetAllRoomByUser(string userID, int page, int pageSize, out int totalRow);
    }
}
