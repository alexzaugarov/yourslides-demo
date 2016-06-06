namespace Yourslides.Data.Infrastructure {
    public class UnitOfWork : IUnitOfWork {
        private readonly IDbFactory _dbFactory;
        private DataStore _dbContext;

        public UnitOfWork(IDbFactory dbFactory) {
            this._dbFactory = dbFactory;
        }

        public DataStore DbContext {
            get { return _dbContext ?? (_dbContext = _dbFactory.Init()); }
        }

        public void Commit() {
            DbContext.Commit();
        } 
    }
}