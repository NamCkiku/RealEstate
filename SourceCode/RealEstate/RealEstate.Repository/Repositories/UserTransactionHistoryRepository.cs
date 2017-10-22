using Dapper;
using RealEstate.Common.Extensions.Data;
using RealEstate.Entities.Entites;
using RealEstate.Entities.ModelView;
using RealEstate.Repository.Infrastructure;
using RealEstate.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Repository.Repositories
{
    public class UserTransactionHistoryRepository : RepositoryBase<UserTransactionHistory>, IUserTransactionHistoryRepository
    {
        public UserTransactionHistoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<UserTransactionHistoryEntity> GetTransactionHistoryByUserId(string userId, int page, int pageSize, out int totalRow)
        {
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@UserID", userId, dbType: DbType.String);
                parameter.Add("@PageCount", pageSize, dbType: DbType.Int32);
                parameter.Add("@PageIndex", page, dbType: DbType.Int32);
                parameter.Add("@totalrow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var result = DapperExtensions.QueryDapperStoreProc<UserTransactionHistoryEntity>("sp_GetTransactionHistoryByUserId", parameter);
                totalRow = parameter.Get<int>("@totalrow");
                return result;
            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message + MethodInfo.GetCurrentMethod().Name);
                totalRow = 0;
                return new List<UserTransactionHistoryEntity>();
            }
        }
    }
}
