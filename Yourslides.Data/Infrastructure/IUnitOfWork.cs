namespace Yourslides.Data.Infrastructure {
    public interface IUnitOfWork {
        void Commit();
        void Reset();
    }
}