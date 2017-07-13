using RealEstate.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service.BaseService
{
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        protected IRepository<T> _repository;
        protected IUnitOfWork _unitOfWork;
        protected BaseService(IRepository<T> repository, IUnitOfWork unitOfWork)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
        }
        public virtual IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }
        public virtual T GetById(int id)
        {
            return _repository.GetSingleById(id);
        }
        public virtual T GetById(string id)
        {
            return _repository.GetById(id);
        }
        public virtual IEnumerable<T> GetMulti(Expression<Func<T, bool>> where)
        {
            return _repository.GetMulti(where);
        }
        public virtual void Update(T entity)
        {
            _repository.Update(entity);
        }
        public virtual T Insert(T entity)
        {
            return _repository.Add(entity);
        }
        public virtual void InsertRange(IEnumerable<T> entities)
        {
            _repository.AddRange(entities);
        }
        public virtual T Delete(int id)
        {
            return _repository.Delete(id);
        }
        public virtual void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
