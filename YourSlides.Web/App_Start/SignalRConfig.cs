using System.Diagnostics;
using Microsoft.AspNet.SignalR;
using Yourslides.FileHandler.Converter;
using YourSlides.Web.Hubs;

namespace YourSlides.Web {
    public class SignalRConfig {
        public static void Configure(IConverter converter) {
            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new MyIdProvider());
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<PresentationProcessing>();
            converter.Started += (sender, args) => hubContext.Clients.User(args.Presentation.OwnerId).onStarted(args.Presentation.Id);
            converter.Processing += (sender, args) => {
                hubContext.Clients.User(args.Presentation.OwnerId).onProgress(args.Presentation.Id, args.CurrentSlide, args.TotalSlides);
            };
            converter.Error += (sender, args) => {
                hubContext.Clients.User(args.OwnerId).onError(args.Presentation.Id);
                Debug.WriteLine("Ошибка. Отправлено клиенту");
            };
            converter.Completed += (sender, args) => hubContext.Clients.User(args.Presentation.OwnerId).onCompleted(args.Presentation.Id);
        }
    }
}