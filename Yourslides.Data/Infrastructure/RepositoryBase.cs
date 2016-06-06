using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Yourslides.Data.Infrastructure {
    public class RepositoryBase<T> : IRepository<T> where T : class {
        #region Properties
        private DataStore _dataContext;
        private readonly IDbSet<T> _dbSet;

        protected IDbFactory DbFactory {
            get;
            private set;
        }

        protected DataStore DbContext {
            get { return _dataContext ?? (_dataContext = DbFactory.Init()); }
        }
        #endregion

        protected RepositoryBase(IDbFactory dbFactory) {
            DbFactory = dbFactory;
            _dbSet = DbContext.Set<T>();
        }

        #region Implementation
        public virtual void Add(T entity) {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity, params Expression<Func<T, object>>[] updatedProperties) {
            _dbSet.Attach(entity);
            var entry = _dataContext.Entry(entity);
            if (updatedProperties.Any()) {
                foreach (var updatedProperty in updatedProperties) {
                    entry.Property(updatedProperty).IsModified = true;
                }
            } else {
                entry.State = EntityState.Modified;
            }
            
        }

        public virtual void Delete(T entity) {
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where) {
            IEnumerable<T> objects = _dbSet.Where(where).AsEnumerable();
            foreach (T obj in objects)
                _dbSet.Remove(obj);
        }

        public virtual T GetById(int id) {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll() {
            return _dbSet.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where) {
            return _dbSet.Where(where).ToList();
        }

        public virtual T Get(Expression<Func<T, bool>> where) {
            return _dbSet.Where(where).FirstOrDefault();
        }

        #endregion
    }
}