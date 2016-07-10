using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using Ninject;
using Yourslides.FileHandler.Converter;
using YourSlides.Web.Hubs;
using YourSlides.Web.Infrastructure;

namespace YourSlides.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundle(BundleTable.Bundles);
            IConverterManager manager = DependencyResolver.Current.GetService<IConverterManager>();
            Task.Run(() => manager.Start());
            SignalRConfig.Configure(DependencyResolver.Current.GetService<IConverter>());
        }
    }
}
