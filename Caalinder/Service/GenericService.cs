using Caalinder.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Caalinder.Service
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public GenericService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public List<string> Delete(int id)
        {
            if (this.Exists(id))
                _repository.Delete(id);
            return new List<string>();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> orderBy = null, string includeProperties = "")
        {

            return _repository.Get(filter, orderBy != null ? orderBy.Compile() : null, includeProperties);
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public T GetById(int id)
        {
            return _repository.GetById(id);
        }

        public T GetByIdNoTracking(int id)
        {
            return _repository.GetByIdNoTracking(id);
        }

        public List<string> Insert(T obj)
        {
            _repository.Insert(obj);
            return new List<string>();
        }

        public List<string> Update(T obj)
        {
            _repository.Update(obj);
            return new List<string>();
        }

        public bool Exists(int id)
        {
            return (_repository.GetById(id) != null) ? true : false;
        }
    }
}