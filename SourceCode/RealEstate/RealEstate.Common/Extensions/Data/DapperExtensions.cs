using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Common.Extensions.Data
{
    public static class DapperExtensions
    {
        internal static string ConnectionString = ConfigurationManager.ConnectionStrings["RealEstateConnection"].ConnectionString;
        public static IEnumerable<T> QueryDapper<T>(string sql, object param = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                return conn.Query<T>(sql, param);
            }
        }
        public static T QueryFirstOrDefault<T>(string sql, object parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                return conn.QueryFirstOrDefault<T>(sql, parameters);
            }
        }
        public static IEnumerable<T> QueryDapperStoreProc<T>(string store, object param = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                return conn.Query<T>(store, param, commandType: CommandType.StoredProcedure);
            }
        }

        public static int ExecuteDapper(string sql, object param = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                return conn.Execute(sql, param);
            }
        }
    }
}
