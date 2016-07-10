using Ninject.Modules;
using Yourslides.Data.Repositories;

namespace YourSlides.Web.Infrastructure {
    public class RepositoryNinjectModule : NinjectModule {
        public override void Load() {
            Bind<IPresentationRepository>().To<PresentationRepository>();
            Bind<ICommentRepository>().To<CommentRepository>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IPresentationWatchRepository>().To<PresentationWatchRepository>();
        }
    }
}