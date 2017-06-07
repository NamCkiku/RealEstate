using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service.BaseService
{
    /// <summary>
    ///  Service định nghĩa giao diện dùng chung cho các Service con
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <Modified>
    /// Name     Date         Comments
    /// namth  6/6/2017   created
    /// </Modified>
    public interface IService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        T GetById(string id);
        IEnumerable<T> GetMulti(Expression<Func<T, bool>> where);
        void Update(T entity);
        T Insert(T entity);
        void InsertRange(IEnumerable<T> entities);
        T Delete(int id);

        void SaveChanges();
    }
}
