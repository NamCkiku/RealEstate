using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Common.Cache
{
    /// <summary>
    /// Cache Provider
    /// </summary>
    /// <Modified>
    /// Name     Date         Comments
    /// trungtq  30/08/2014   created
    /// </Modified>
    public interface ICacheProvider
    {
        /// <summary>
        /// Lấy giá trị trong Cache theo key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key) where T : class;

        /// <summary>
        /// Remove giá trị trong Cache theo Key
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);

        /// <summary>
        /// Thêm vào Cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="time"></param>
        void Insert<T>(string key, T obj, int time) where T : class;

        /// <summary>
        /// Kiểm tra Cache 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Contains(string key);

        /// <summary>
        /// Lấy hết key ra.
        /// </summary>
        /// <returns></returns>
        IEnumerator<string> GetKeysEnumerator();
    }
}
