using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RealEstate.Common.Cache
{
    /// <summary>
    /// Cache sử dụng Web
    /// </summary>
    /// <Modified>
    /// Name     Date         Comments
    /// trungtq  30/08/2014   created
    /// </Modified>
    public class WebCacheProvider : ICacheProvider
    {
        private System.Web.Caching.Cache _cache = HttpRuntime.Cache;

        /// <summary>
        /// Lấy giá trị trong Cache theo key
        /// </summary>
        /// <Modified>
        /// Name     Date         Comments
        /// trungtq  30/08/2014   created
        /// </Modified>
        public T Get<T>(string key) where T : class
        {
            return _cache[key] as T;
        }

        /// <summary>
        /// Remove giá trị trong Cache theo Key
        /// </summary>
        /// <Modified>
        /// Name     Date         Comments
        /// trungtq  30/08/2014   created
        /// </Modified>
        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        /// <summary>
        /// Thêm vào Cache
        /// </summary>
        /// <Modified>
        /// Name     Date         Comments
        /// trungtq  30/08/2014   created
        /// </Modified>
        public void Insert<T>(string key, T obj, int time) where T : class
        {
            _cache.Insert(key, obj, null, DateTime.Now.AddMinutes(time), TimeSpan.Zero);
        }

        /// <summary>
        /// Kiểm tra Cache
        /// </summary>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// trungtq  30/08/2014   created
        /// </Modified>
        public bool Contains(string key)
        {
            return _cache[key] != null;
        }

        /// <summary>
        /// Lấy hết key ra.
        /// </summary>
        /// <Modified>
        /// Name     Date         Comments
        /// trungtq  30/08/2014   created
        /// </Modified>
        public IEnumerator<string> GetKeysEnumerator()
        {
            return _cache.Cast<DictionaryEntry>().Select(d => d.Key.ToString()).GetEnumerator();
        }
    }
}
