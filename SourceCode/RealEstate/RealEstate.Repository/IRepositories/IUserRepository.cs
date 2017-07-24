using Microsoft.AspNet.Identity;
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
    public interface IUserRepository : IRepository<AppUser>
    {
        Task<IdentityResult> RegisterUser(AppUser userModel);

        Task<IEnumerable<AppUserEntity>> GetAllUserIsBirthDay();
    }
}
