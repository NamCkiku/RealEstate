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
    }
}
