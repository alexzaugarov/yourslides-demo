using Ninject.Modules;
using Ninject.Web.Common;
using Yourslides.Data.Infrastructure;
using Yourslides.Service;

namespace YourSlides.Web.Infrastructure {
    public class CommonNinjectModule : NinjectModule {
        public override void Load() {
            Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            Bind<IDbFactory>().To<DbFactory>().InRequestScope();
        }
    }
}