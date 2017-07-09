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
    public class WardRepository : RepositoryBase<Ward>, IWardRepository
    {
        public WardRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<WardEntity> GetAllWard()
        {
            try
            {
                return DapperExtensions.QueryDapperStoreProc<WardEntity>("sp_GetAllWard").ToList();

            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message + MethodInfo.GetCurrentMethod().Name);
                return new List<WardEntity>();
            }
        }

        public IEnumerable<WardEntity> GetAllWardByDistrictId(int id)
        {
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Id", id, dbType: DbType.Int32);
                return DapperExtensions.QueryDapperStoreProc<WardEntity>("sp_GetAllWardByDistrictId", parameter).ToList();

            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message + MethodInfo.GetCurrentMethod().Name);
                return new List<WardEntity>();
            }
        }
    }
}
