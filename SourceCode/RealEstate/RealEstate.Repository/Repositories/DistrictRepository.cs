using RealEstate.Common.Extensions.Data;
using RealEstate.Entities.Entites;
using RealEstate.Entities.ModelView;
using RealEstate.Repository.Infrastructure;
using RealEstate.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Repository.Repositories
{
    public class DistrictRepository : RepositoryBase<District>, IDistrictRepository
    {
        public DistrictRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<DistrictEntity> GetAllDistrict()
        {
            try
            {
                return DapperExtensions.QueryDapperStoreProc<DistrictEntity>("sp_GetAllDistrict").ToList();

            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message + MethodInfo.GetCurrentMethod().Name);
                return new List<DistrictEntity>();
            }
        }
    }
}
