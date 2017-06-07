using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Common.Extensions.Data
{
    /// <summary>
    /// Mở rộng cho Entity Framework code first sử dụng được stored procedure
    /// Class chỉ làm việc với trường hợp gọi stored procedure đơn giản, không hỗ trợ output
    /// http://code-clarity.blogspot.com/2012/02/entity-framework-code-first-easy-way-to.html
    /// </summary>
    /// <Modified>
    /// Name     Date         Comments
    /// namth  7/7/2017  created
    /// </Modified>
    public static class DatabaseExtensions
    {
        /// <summary>
        /// Chạy store dạng NoneQuery
        /// </summary>
        /// <param name="self"></param>
        /// <param name="storedProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteSqlCommandSmart(this Database self, string storedProcedure, object parameters = null)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (string.IsNullOrEmpty(storedProcedure))
                throw new ArgumentException("storedProcedure");

            var arguments = PrepareArguments(storedProcedure, parameters);
            return self.ExecuteSqlCommand(arguments.Item1, arguments.Item2);
        }

        /// <summary>
        /// Chạy stored trả về dữ liệu dạng IEnumerable
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="self"></param>
        /// <param name="storedProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IEnumerable<TElement> ExecuteSqlStoredProcedure<TElement>(this Database self, string storedProcedure, object parameters = null)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (string.IsNullOrEmpty(storedProcedure))
                throw new ArgumentException("storedProcedure");

            var arguments = PrepareArguments(storedProcedure, parameters);
            return self.SqlQuery<TElement>(arguments.Item1, arguments.Item2);
        }

        /// <summary>
        /// Chạy stored trả về dữ liệu dạng IEnumerable
        /// </summary>
        /// <param name="self"></param>
        /// <param name="elementType"></param>
        /// <param name="storedProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IEnumerable ExecuteSqlStoredProcedure(this Database self, Type elementType, string storedProcedure, object parameters = null)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (elementType == null)
                throw new ArgumentNullException("elementType");
            if (string.IsNullOrEmpty(storedProcedure))
                throw new ArgumentException("storedProcedure");

            var arguments = PrepareArguments(storedProcedure, parameters);
            return self.SqlQuery(elementType, arguments.Item1, arguments.Item2);
        }

        /// <summary>
        /// Chuẩn bị tham số đầu vào cho stored procedure
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static Tuple<string, object[]> PrepareArguments(string storedProcedure, object parameters)
        {
            var parameterNames = new List<string>();
            var parameterParameters = new List<object>();

            if (parameters != null)
            {
                foreach (PropertyInfo propertyInfo in parameters.GetType().GetProperties())
                {
                    string name = "@" + propertyInfo.Name;
                    object value = propertyInfo.GetValue(parameters, null);

                    parameterNames.Add(name);
                    parameterParameters.Add(new SqlParameter(name, value ?? DBNull.Value));
                }
            }

            if (parameterNames.Count > 0)
                storedProcedure += " " + string.Join(", ", parameterNames);

            return new Tuple<string, object[]>(storedProcedure, parameterParameters.ToArray());
        }
    }
}
