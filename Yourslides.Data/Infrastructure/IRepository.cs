using System;
using System.Linq;
using System.Linq.Expressions;

namespace Yourslides.Data.Infrastructure {
    public interface IRepository<T> where T: class {
        // Marks an entity as new
        void Add(T entity);
        // Marks an entity to be removed
        void Delete(T entity);
        // Get an entity by int id
        T GetById(long id);
        // Gets entities using delegate
        IQueryable<T> GetMany(Expression<Func<T, bool>> where);
    }
}