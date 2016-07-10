namespace Yourslides.Data.Infrastructure {
    public class DbFactory : Disposable, IDbFactory {
        private DataStore _dbContext;

        public DataStore Instance() {
            return _dbContext ?? (_dbContext = new DataStore());
        }
        public void Reset() {
            DisposeCore();
            _dbContext = new DataStore();
        }

        protected override void DisposeCore() {
            if (_dbContext != null)
                _dbContext.Dispose();
        } 
    }
}