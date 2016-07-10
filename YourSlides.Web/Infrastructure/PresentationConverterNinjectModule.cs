using Ninject.Modules;
using Yourslides.Data.Infrastructure;
using Yourslides.Data.Repositories;
using Yourslides.FileHandler.Converter;
using Yourslides.FileHandler.GhostscriptHelpers;
using Yourslides.FileHandler.Tools;
using Yourslides.Service;

namespace YourSlides.Web.Infrastructure {
    public class PresentationConverterNinjectModule : NinjectModule {
        public override void Load() {
            var dbFactory = new DbFactory();
            var unitOfWork = new UnitOfWork(dbFactory);
            var presentationRepository = new PresentationRepository(dbFactory);
            var presentationWatchRepository = new PresentationWatchRepository(dbFactory);
            var presentationService = new PresentationService(unitOfWork, presentationRepository, presentationWatchRepository);
            Bind<IPresentationService>().ToConstant(presentationService).WhenInjectedInto<ConverterManager>();
            /*Bind<IPresentationService>().To<PresentationService>().WhenInjectedInto<Converter>().Named("ToConverter");
            Bind<IUnitOfWork>().To<UnitOfWork>().WhenParentNamed("ToConverter").Named("UnitOfWorkToConverter");
            Bind<IDbFactory>().To<DbFactory>().WhenParentNamed("UnitOfWorkToConverter");*/
            Bind<IConverterManager, IConverterService>().To<ConverterManager>().InSingletonScope();
            Bind<IConverter>().To<Converter>().InSingletonScope();
            Bind<GhostscriptProcessorFactory>().ToSelf();
            Bind<IGhostscriptImageDeviceFactory>().To<GhostscriptPngDeviceFactory>();
            Bind<IImageProcessor>().To<ImageProcessor>();
        }
    }
}