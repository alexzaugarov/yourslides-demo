using Yourslides.Data.Infrastructure;

namespace Yourslides.Service {
    public interface IService {
        void Save();
    }
    public abstract class ServiceBase : IService {
        private readonly IUnitOfWork _unitOfWork;

        protected ServiceBase(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        public virtual void Save() {
            _unitOfWork.Commit();
        }
    }
}