namespace Yourslides.Data.Infrastructure {
    public class UnitOfWork : IUnitOfWork {
        private readonly IDbFactory _dbFactory;

        public UnitOfWork(IDbFactory dbFactory) {
            this._dbFactory = dbFactory;
        }

        public DataStore DbContext {
            get { return _dbFactory.Instance(); }
        }

        public void Commit() {
            DbContext.Commit();
        }

        public void Reset() {
            _dbFactory.Reset();
        }
    }
}