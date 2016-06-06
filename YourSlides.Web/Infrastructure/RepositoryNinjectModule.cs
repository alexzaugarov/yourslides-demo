using Ninject.Modules;
using Yourslides.Data.Repositories;

namespace YourSlides.Web.Infrastructure {
    public class RepositoryNinjectModule : NinjectModule {
        public override void Load() {
            Bind<IPresentationRepository>().To<PresentationRepository>();
        }
    }
}