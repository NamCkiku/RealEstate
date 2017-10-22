using Dapper;
using RealEstate.Common.Extensions.Data;
using RealEstate.Entities.Entites;
using RealEstate.Entities.ModelView;
using RealEstate.Repository.Infrastructure;
using RealEstate.Repository.IRepositories;
using System;
using System.Data;
using System.Reflection;

namespace RealEstate.Repository.Repositories
{
    public class UserWalletRepository : RepositoryBase<UserWallet>, IUserWalletRepository
    {
        public UserWalletRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public UserWalletEntity GetWalletByUserID(string userId)
        {
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@UserID", userId, dbType: DbType.String);
                var result = DapperExtensions.QueryFirstOrDefaultStoreProc<UserWalletEntity>("sp_GetWalletByUserID", parameter);
                return result;
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message + MethodInfo.GetCurrentMethod().Name);
                return new UserWalletEntity();
            }
        }
    }
}
