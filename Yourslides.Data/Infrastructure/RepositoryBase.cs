using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Yourslides.Data.Infrastructure {
    public class RepositoryBase<T> : IRepository<T> where T : class {
        #region Properties

        protected IDbFactory DbFactory {
            get;
            private set;
        }

        protected DataStore DbContext {
            get { return DbFactory.Instance(); }
        }
        protected DbSet<T> DbSet {
            get {return DbContext.Set<T>();}
        } 
        #endregion

        protected RepositoryBase(IDbFactory dbFactory) {
            DbFactory = dbFactory;
        }

        #region Implementation
        public virtual void Add(T entity) {
            DbSet.Add(entity);
        }

        public virtual void Delete(T entity) {
            DbContext.Entry(entity).State = EntityState.Deleted;
        }

        public virtual T GetById(long id) {
            return DbSet.Find(id);
        }

        public virtual IQueryable<T> GetMany(Expression<Func<T, bool>> where) {
            return DbSet.Where(where);
        }
        #endregion
    }
}