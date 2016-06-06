using AutoMapper;
using Ninject.Modules;

namespace YourSlides.Web.Mappings {
    public class InjectConfigurationMapping : NinjectModule {
        public override void Load() {
            var config = new MapperConfiguration(x => {
                x.AddProfile<DomainToViewModelMappingProfile>();
            });
            var mapper = config.CreateMapper();
            Bind<IMapper>().ToConstant(mapper);
        }
    }
}