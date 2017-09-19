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
using Dapper;
using System.Data;

namespace RealEstate.Repository.Repositories
{
    public class UserRepository : RepositoryBase<AppUser>, IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<AuditlogEntity> GetAllHistoryLogin(string userID, int page, int pageSize, out int totalRow)
        {
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@UserID", userID, dbType: DbType.String);
                parameter.Add("@PageCount", pageSize, dbType: DbType.Int32);
                parameter.Add("@PageIndex", page, dbType: DbType.Int32);
                parameter.Add("@totalrow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var result = DapperExtensions.QueryDapperStoreProc<AuditlogEntity>("sp_HistoryLogin", parameter);
                totalRow = parameter.Get<int>("@totalrow");
                return result;
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message + MethodInfo.GetCurrentMethod().Name);
                totalRow = 0;
                return new List<AuditlogEntity>();
            }
        }

        public async Task<IEnumerable<AppUserEntity>> GetAllUserIsBirthDay()
        {
            try
            {
                var result = await DapperExtensions.QueryDapperStoreProcAsync<AppUserEntity>("GetBirthdayBuddiesEmails");
                return result;
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message + MethodInfo.GetCurrentMethod().Name);
                return new List<AppUserEntity>();
            }
        }

        public AppUserEntity GetInfomationUserById(string userID)
        {
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@UserID", userID, dbType: DbType.String);
                var result = DapperExtensions.QueryFirstOrDefaultStoreProc<AppUserEntity>("sp_GetInfomationUserById", parameter);
                return result;
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message + MethodInfo.GetCurrentMethod().Name);
                return new AppUserEntity();
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
