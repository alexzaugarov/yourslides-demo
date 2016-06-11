using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject;
using Yourslides.FileHandler.Converter;
using YourSlides.Web.Infrastructure;

namespace YourSlides.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundle(BundleTable.Bundles);
            IConverterManager manager = DependencyResolver.Current.GetService<IConverterManager>();
            Task.Run(() => manager.Start());
        }
    }
}
