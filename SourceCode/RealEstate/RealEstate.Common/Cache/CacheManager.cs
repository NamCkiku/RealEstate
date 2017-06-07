using RealEstate.Common.Helper;
using RealEstate.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Common.Cache
{
    /// <summary>
    /// All references to Cache should go this class
    /// </summary>
    /// <Modified>
    /// Name     Date         Comments
    /// trungtq  1/3/2012   created
    /// </Modified>
    public class CacheManager
    {
        /// <summary>
        /// true: enable cache
        /// false: diable cache (when you need test.)
        /// </summary>
        private static bool enabled = true;

        private static object tobj = new object();

        /// <summary>
        /// Thoi gian cache mac dinh (15')
        /// </summary>
        public const int CACHE_DEFAULT_TIME = 15;

        /// <summary>
        /// The _cache.
        /// </summary>
        private ICacheProvider _cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="GpsCache"/> class. 
        /// Default constuctor uses HttpContext.Current as source for obtaining Cache object
        /// </summary>
        public CacheManager()
        {
            if (_cache == null)
            {
                // Nếu sử dụng Cache Web thường
                _cache = new WebCacheProvider();

            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GpsCache"/> class. 
        /// Initializes class with specified Cache object
        /// </summary>
        /// <param name="cache">
        /// Cache to work with
        /// </param>
        public CacheManager(ICacheProvider cache)
        {
            _cache = cache;
        }

        public static CacheManager Instance => Singleton<CacheManager>.Instance;

        /// <summary>
        /// set cache voi mot key xac dinh va thoi gian 
        /// </summary>
        /// <param name="cacheKey">cache key</param>
        /// <param name="obj">object can cahe</param>
        /// <param name="time">cach trong bao nhieu phu't</param>
        public void SetCache<T>(string cacheKey, T obj, int time) where T : class
        {
            if (!enabled) return;
            lock (tobj)
            {
                if (obj == null) return;
                _cache.Remove(cacheKey);
                _cache.Insert(cacheKey, obj, time);
            }
        }

        /// <summary>
        /// set cache voi thoi gian mac dinh
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="obj"></param>
        public void SetCache<T>(string cacheKey, T obj) where T : class
        {
            SetCache(cacheKey, obj, CACHE_DEFAULT_TIME);
        }

        /// <summary>
        /// set cache voi key va group
        /// </summary>
        /// <param name="keys">list cac object tham gia vao cahekey </param>
        /// <param name="obj"> object can cache</param>
        public void SetCacheByKeys<T>(T obj, int time, params object[] keys) where T : class
        {
            if (!enabled) return;
            if (obj == null) return;
            string key = ConstructCacheKey(keys);
            if (!string.IsNullOrEmpty(key)) SetCache(key, obj, time);
        }

        /// <summary>
        /// set cache voi key va group
        /// </summary>
        /// <param name="keys">list cac object tham gia vao cahekey </param>
        /// <param name="obj"> object can cache</param>
        public void SetCacheByKeys<T>(T obj, params object[] keys) where T : class
        {
            SetCacheByKeys<T>(obj, CACHE_DEFAULT_TIME, keys);
        }

        /// <summary>
        /// get cache voi key va groups
        /// </summary>
        /// <param name="keys">list cac object tham gia vao cahekey </param>
        /// <returns></returns>
        public T GetCacheByKeys<T>(params object[] keys) where T : class
        {
            if (!enabled) return default(T);
            string ckey = ConstructCacheKey(keys);
            return GetCache<T>(ckey);

        }

        /// <summary>
        /// Get cache voi key xac dinh
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public T GetCache<T>(string cacheKey) where T : class
        {
            if (!enabled) return default(T);
            lock (tobj)
            {
                object obj = _cache.Get<T>(cacheKey);

                return (obj != null && obj is T) ? (T)obj : default(T);
            }
        }

        /// <summary>
        /// xoa cache co key xac dinh
        /// </summary>
        /// <param name="cacheKey"></param>
        public void RemoveCache(string cacheKey)
        {
            if (!enabled) return;

            lock (tobj)
            {
                _cache.Remove(cacheKey);
            }
        }

        /// <summary>
        /// remove all cache
        /// </summary>
        public void RemoveAll()
        {
            lock (tobj)
            {
                var ce = _cache.GetKeysEnumerator();
                while (ce.MoveNext()) _cache.Remove(ce.Current);
            }
        }

        /// <summary>
        /// xoa cache neu key chua list cac object xac dinh
        /// </summary>
        /// <param name="oKeys">list cac object tham gia vao cachekey </param>
        public void RemoveCacheByKeys(params object[] oKeys)
        {
            if (!enabled) return;

            if (oKeys == null || oKeys.Length <= 0) return;

            string[] keys = GetCacheItemStrings(oKeys);

            lock (tobj)
            {
                var ce = _cache.GetKeysEnumerator();

                while (ce.MoveNext())
                {
                    string key = ce.Current;
                    bool ok = true;
                    foreach (string k in keys)
                    {
                        if (!string.IsNullOrEmpty(k))
                        {
                            if (!key.Contains(k, StringComparison.InvariantCultureIgnoreCase))
                            {
                                ok = false;
                                break;
                            }
                        }
                    }

                    if (ok)
                    {
                        _cache.Remove(key);
                    }
                }
            }
        }

        /// <summary>
        /// xoa cache neu trong cache key chua cac xau kt nao do
        /// </summary>
        /// <param name="keys"></param>
        public void RemoveCacheIfKeyContains(params string[] keys)
        {
            if (!enabled) return;

            if (keys == null || keys.Length == 0) return;
            lock (tobj)
            {
                var ce = _cache.GetKeysEnumerator();

                while (ce.MoveNext())
                {
                    string key = ce.Current;
                    foreach (string k in keys)
                    {
                        if (!string.IsNullOrEmpty(k))
                        {
                            if (key.Contains(k, StringComparison.InvariantCultureIgnoreCase))
                            {
                                // Remove cache luon.
                                _cache.Remove(key);

                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// trungtq: chuyen thang lay ra gia tri default khi thang key bi null.
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        private string[] GetCacheItemStrings(object[] arr)
        {
            if (arr == null || arr.Length == 0) return null;

            string[] keys = new string[arr.Length];
            for (int i = 0; i < arr.Length; ++i)
            {
                object ob = TypeHelper.GetDefaultValue<object>(arr[i]);

                if (ob is CacheKeys)
                {
                    keys[i] = "[" + ob.ToString().ToLower() + "]";
                }
                else keys[i] = "(" + ob.ToString().ToLower() + ")";
            }
            return keys;

        }

        public string ConstructCacheKey(params object[] objs)
        {
            string[] keys = GetCacheItemStrings(objs);

            if (keys == null) return string.Empty;

            return string.Join("_", keys);
        }

        /// <summary>
        /// Kiem tra cache key da ton tai hay chua?
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool CheckCacheKey(params string[] keys)
        {
            bool ok = false;
            if (!enabled) return false;

            if (keys == null || keys.Length == 0) return false;

            lock (tobj)
            {
                var ce = _cache.GetKeysEnumerator();

                while (ce.MoveNext())
                {
                    string key = ce.Current;
                    foreach (string k in keys)
                    {
                        if (!string.IsNullOrEmpty(k))
                            if (key.Contains(k))
                            {
                                ok = true;
                                break;
                            }
                    }
                }
            }
            return ok;
        }

        /// <summary>
        /// Removes all keys su dung LINQ.
        /// Remove((x) => x.StartsWith(Sanotc.Utility.CacheGroups.GeneralPurpose))
        /// </summary>
        /// <param name="predicate">
        /// Predicate for matching cache keys.
        /// </param>
        public void Remove(Predicate<string> predicate)
        {
            // GetEnumerator
            var key = _cache.GetKeysEnumerator();

            // cycle through cache keys
            while (key.MoveNext())
            {
                // remove cache item if predicate returns true
                if (predicate(key.Current))
                {
                    _cache.Remove(key.Current);
                }
            }
        }

        /// <summary>
        /// Removes all cache items that start with the string.
        /// </summary>
        /// <param name="startsWith">
        /// </param>
        public void RemoveAllStartsWith(string startsWith)
        {
            Remove(x => x.StartsWith(startsWith));
        }


        /// <summary>
        /// Lấy ra tất cả các keys trong cache
        /// </summary>
        /// <returns></returns>
        public string GetAllKeys()
        {
            List<string> keys = null;
            lock (tobj)
            {
                keys = new List<string>();
                var ce = _cache.GetKeysEnumerator();
                while (ce.MoveNext())
                {
                    keys.Add(ce.Current);
                }
            }
            return (keys != null && keys.Count > 0) ? string.Join("<br/> - ", keys) : string.Empty;
        }
    }
}
