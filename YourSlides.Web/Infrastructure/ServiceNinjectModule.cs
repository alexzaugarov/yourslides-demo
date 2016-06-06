using Ninject.Modules;
using Ninject.Web.Common;
using Yourslides.FileHandler.Service;
using Yourslides.Service;

namespace YourSlides.Web.Infrastructure {
    public class ServiceNinjectModule : NinjectModule {
        public override void Load() {
            Bind<IFileService>().To<FileService>().InRequestScope();
            Bind<IPresentationService>().To<PresentationService>();
        }
    }
}