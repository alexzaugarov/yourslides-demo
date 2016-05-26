using System;
using Infrastructure;
using Infrastructure.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using YourSlides;

[assembly: OwinStartup(typeof(Startup))]

namespace YourSlides {
    public class Startup {
        public void Configuration(IAppBuilder app) {
            app.CreatePerOwinContext(() => new MainContext());
            app.CreatePerOwinContext<UserManager<User>>((options, context) => {
                var manager = new UserManager<User>(new UserStore<User>(context.Get<MainContext>()));
                manager.UserValidator = new UserValidator<User>(manager);
                return manager;
            });
            app.CreatePerOwinContext<RoleManager<Role>>((options, context) =>
                new RoleManager<Role>(new RoleStore<Role>(context.Get<MainContext>())));

            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Auth/Signin"),
                ExpireTimeSpan = TimeSpan.FromDays(365)
            });
        }
    }
}
