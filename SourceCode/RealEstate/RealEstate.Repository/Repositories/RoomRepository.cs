using RealEstate.Entities.Entites;
using RealEstate.Repository.Infrastructure;
using RealEstate.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Entities.ModelView;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using RealEstate.Common.Extensions.Data;
using System.Reflection;

namespace RealEstate.Repository.Repositories
{
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["RealEstateConnection"].ConnectionString);
        public RoomRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<RoomEntity> GetAllRoom()
        {
            return _db.Query<RoomEntity>("SELECT * FROM ROOM");
        }
        public IEnumerable<RoomEntity> GetAllRoomPagingFullSearch(SearchRoomEntity filter, int page, int pageSize, out int totalRow, string sort)
        {
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Province", filter.ProvinceID, dbType: DbType.Int32);
                parameter.Add("@District", filter.DistrictID, dbType: DbType.Int32);
                parameter.Add("@Ward", filter.WardID, dbType: DbType.Int32);
                parameter.Add("@PriceFrom", filter.PriceFrom, dbType: DbType.Int32);
                parameter.Add("@PriceTo", filter.PriceTo, dbType: DbType.Int32);
                parameter.Add("@PageCount", pageSize, dbType: DbType.Int32);
                parameter.Add("@PageIndex", page, dbType: DbType.Int32);
                parameter.Add("@Sort", sort, dbType: DbType.String);
                parameter.Add("@totalrow", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var result = DapperExtensions.QueryDapperStoreProc<RoomEntity>("sp_GetAllRoomPagingFullSearch", parameter).ToList();

                totalRow = parameter.Get<int>("@totalrow");

                return result;

            }
            catch (Exception ex)
            {
                Common.Logs.LogCommon.WriteLogError(ex.Message + MethodInfo.GetCurrentMethod().Name);
                totalRow = 0;
                return new List<RoomEntity>();
            }
        }
    }
}
