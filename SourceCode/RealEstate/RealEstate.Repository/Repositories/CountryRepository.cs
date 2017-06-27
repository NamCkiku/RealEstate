using Dapper;
using RealEstate.Common.Extensions.Data;
using RealEstate.Entities.Entites;
using RealEstate.Repository.Infrastructure;
using RealEstate.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

        public IEnumerable<Country> GetAllCountryDapper()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["RealEstateConnection"].ConnectionString))
            {
                return db.Query<Country>("SELECT * FROM COUNTRY");
            }
        }
    }
}
