using RealEstate.Entities.Entites;
using RealEstate.Repository.Infrastructure;
using RealEstate.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RealEstate.Common.Extensions.Data;
using System.Reflection;
using RealEstate.Entities.ModelView;

namespace RealEstate.Repository.Repositories
{
    public class UserRepository : RepositoryBase<AppUser>, IUserRepository
    {
        private UserManager<IdentityUser> _userManager;
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public async Task<IEnumerable<AppUserEntity>> GetAllUserIsBirthDay()
        {
            try
            {
                var result= await DapperExtensions.QueryDapperStoreProcAsync<AppUserEntity>("GetBirthdayBuddiesEmails");
                return result;
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message + MethodInfo.GetCurrentMethod().Name);
                return new List<AppUserEntity>();
            }
        }

        public async Task<IdentityResult> RegisterUser(AppUser userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.PasswordHash);

            return result;
        }

    }
}
