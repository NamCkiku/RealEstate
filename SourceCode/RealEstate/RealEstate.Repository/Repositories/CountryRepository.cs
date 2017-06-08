using RealEstate.Common.Extensions.Data;
using RealEstate.Entities.Entites;
using RealEstate.Repository.Infrastructure;
using RealEstate.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Repository.Repositories
{
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        public CountryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Country> GetAllCountry()
        {
            try
            {
                return DbContext.Database.ExecuteSqlStoredProcedure<Country>("[spGetAllCountry]", new
                {
                }).ToList();

            }
            catch (Exception ex)
            {
                //log.FatalFormat("{0} has an exception:{1}", MethodInfo.GetCurrentMethod().Name, ex);
                return new List<Country>();
            }
        }
    }
}
