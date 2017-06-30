using RealEstate.Common.Extensions.Data;
using RealEstate.Entities.Entites;
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
    public class WardRepository : RepositoryBase<Ward>, IWardRepository
    {
        public WardRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Ward> GetAllWard()
        {
            try
            {
                return DapperExtensions.QueryDapper<Ward>("SELECT * FROM WARD").ToList();

            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message + MethodInfo.GetCurrentMethod().Name);
                return new List<Ward>();
            }
        }
    }
}
