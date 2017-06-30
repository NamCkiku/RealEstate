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
    public class ProvinceRepository : RepositoryBase<Province>, IProvinceRepository
    {
        public ProvinceRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Province> GetAllProvince()
        {
            try
            {
                return DapperExtensions.QueryDapper<Province>("SELECT * FROM PROVINCE").ToList();

            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message + MethodInfo.GetCurrentMethod().Name);
                return new List<Province>();
            }
        }
    }
}
