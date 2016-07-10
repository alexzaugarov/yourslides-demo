using System.Linq;
using Yourslides.Data.Infrastructure;
using Yourslides.Model;

namespace Yourslides.Data.Repositories {
    public interface IPresentationWatchRepository :IRepository<PresentationWatch> {
        bool TryAdd(PresentationWatch watch);
    }
    public class PresentationWatchRepository: RepositoryBase<PresentationWatch>, IPresentationWatchRepository{
        public PresentationWatchRepository(IDbFactory dbFactory) : base(dbFactory) {
        }

        public bool TryAdd(PresentationWatch watch) {
            if (!DbSet.Any(w => w.PresentationId == watch.PresentationId && w.VisitorId.Equals(watch.VisitorId))) {
                base.Add(watch);
                return true;
            }
            return false;
        }
    }
}