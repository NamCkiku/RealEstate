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
using System.Reflection;
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
                Common.Logs.LogCommon.WriteLogError(ex.Message + MethodInfo.GetCurrentMethod().Name);
                return new List<Country>();
            }
        }

        public IEnumerable<Country> GetAllCountryDapper()
        {
            try
            {
                return DapperExtensions.QueryDapperStoreProc<Country>("spGetAllCountry").ToList();

            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message + MethodInfo.GetCurrentMethod().Name);
                return new List<Country>();
            }
        }
    }
}
