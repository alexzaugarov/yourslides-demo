namespace Yourslides.Data.Infrastructure {
    public class DbFactory : Disposable, IDbFactory {
        private DataStore _dbContext;

        public DataStore Init() {
            return _dbContext ?? (_dbContext = new DataStore());
        }

        protected override void DisposeCore() {
            if (_dbContext != null)
                _dbContext.Dispose();
        } 
    }
}