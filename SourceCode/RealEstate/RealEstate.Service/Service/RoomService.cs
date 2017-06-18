using RealEstate.Entities.Entites;
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
    public class RoomService : BaseService<Room>, IRoomService
    {
        public RoomService(IRepository<Room> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
