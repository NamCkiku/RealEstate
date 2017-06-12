using RealEstate.Entities.Entites;
using RealEstate.Entities.ModelView;
using RealEstate.Repository.Infrastructure;
using RealEstate.Service.BaseService;
using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service.Service
{
    public class RoomTypeService : BaseService<RoomType>, IRoomTypeService
    {
        public RoomTypeService(IRepository<RoomType> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        public IEnumerable<RoomType> GetAllRoomType(SearchRoomTypeEntity filter)
        {
            List<RoomType> lstroomType = new List<RoomType>();
            DateTime st = filter.StartDate == null ? DateTime.MinValue : filter.StartDate.Value.Date;
            DateTime et = filter.EndDate == null ? DateTime.MaxValue : filter.EndDate.Value.Date.AddDays(1);
            if (filter.Keywords == null)
            {
                filter.Keywords = "";
            }
            try
            {
                lstroomType = _repository.GetMulti(x => x.Status == filter.Status
                && (filter.StartDate == null || x.CreatedDate >= st || x.CreatedDate == null)
                && (filter.EndDate == null || x.CreatedDate < et || x.CreatedDate == null)
                && (x.RoomTypeName.Contains(filter.Keywords) || x.Description.Contains(filter.Keywords) || string.IsNullOrEmpty(filter.Keywords))
                ).OrderByDescending(x => x.CreatedDate).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstroomType;
        }
    }
}
