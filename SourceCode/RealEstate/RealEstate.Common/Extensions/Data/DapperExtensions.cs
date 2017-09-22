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
                var result = conn.Query<T>(sql, param);
                conn.Close();
                return result;
            }
        }
        public static T QueryFirstOrDefault<T>(string sql, object parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var result = conn.QueryFirstOrDefault<T>(sql, parameters);
                conn.Close();
                return result;
            }
        }
        public static T QueryFirstOrDefaultStoreProc<T>(string store, object parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var result = conn.QueryFirstOrDefault<T>(store, parameters, commandType: CommandType.StoredProcedure);
                conn.Close();
                return result;
            }
        }
        public static IEnumerable<T> QueryDapperStoreProc<T>(string store, object param = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var result = conn.Query<T>(store, param, commandType: CommandType.StoredProcedure);
                conn.Close();
                return result;
            }
        }

        public static int ExecuteDapper(string sql, object param = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var result = conn.Execute(sql, param);
                conn.Close();
                return result;
            }
        }


        public static async Task<IEnumerable<T>> QueryDapperAsync<T>(string sql, object param = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var result = await conn.QueryAsync<T>(sql, param);
                conn.Close();
                return result;
            }
        }
        public static async Task<IEnumerable<T>> QueryDapperStoreProcAsync<T>(string store, object param = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var result = await conn.QueryAsync<T>(store, param, commandType: CommandType.StoredProcedure);
                conn.Close();
                return result;
            }
        }
        public static async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var result = await conn.QueryFirstOrDefaultAsync<T>(sql, parameters);
                conn.Close();
                return result;
            }
        }
        public static async Task<T> QueryFirstOrDefaultStoreProcAsync<T>(string store, object parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var result = await conn.QueryFirstOrDefaultAsync<T>(store, parameters, commandType: CommandType.StoredProcedure);
                conn.Close();
                return result;
            }
        }
        public static async Task<int> ExecuteDapperAsync(string sql, object param = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var result = await conn.ExecuteAsync(sql, param);
                conn.Close();
                return result;
            }
        }
    }
}
