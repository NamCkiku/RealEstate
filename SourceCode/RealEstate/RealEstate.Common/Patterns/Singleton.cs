using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Common.Patterns
{
    /// <summary>
    /// Singleton Patterns
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <Modified>
    /// Name     Date         Comments
    /// trungtq  26/6/2013   created
    /// </Modified>
    public class Singleton<T> where T : class, new()
    {
        private static T _item;
        private static object _syncRoot = new object();

        /// <summary>
        /// Get the instance of the singleton item T.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_item == null)
                {
                    lock (_syncRoot)
                    {
                        _item = new T();
                    }
                }
                return _item;
            }
        }
    }
}
