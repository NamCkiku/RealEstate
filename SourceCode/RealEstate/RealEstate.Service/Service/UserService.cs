using RealEstate.Entities.Entites;
using RealEstate.Service.BaseService;
using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Repository.Infrastructure;

namespace RealEstate.Service.Service
{
    public class UserService : BaseService<AppUser>, IUserService
    {
        public UserService(IRepository<AppUser> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
