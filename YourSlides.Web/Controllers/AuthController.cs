using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Yourslides.Model.Account;
using YourSlides.Web.Attributes;
using YourSlides.Web.Models.Auth;

namespace YourSlides.Web.Controllers {

    public class AuthController : Controller {
        private UserManager<User> UserManager {
            get {
                return HttpContext.GetOwinContext().GetUserManager<UserManager<User>>();
            }
        }

        private IAuthenticationManager AuthenticationManager {
            get {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: Auth
        [AnonymousOnly]
        public ActionResult Signin() {
            return View();
        }
        [AnonymousOnly]
        public ActionResult Signup() {
            return View();
        }

        //POST: Auth
        [HttpPost]
        [AnonymousOnly]
        public ActionResult Signin(SigninPage model) {
            if (!ModelState.IsValid) {
                return View();
            }

            var user = UserManager.Find(model.UserName, model.Password);

            if (user != null) {
                SignIn(user, model.IsPersistent);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Неверное имя пользователя или пароль");
            return View();
        }
        [HttpPost]
        [AnonymousOnly]
        public ActionResult Signup(SignupPage model, string returnUrl) {
            if (!ModelState.IsValid) {
                return View();
            }
            if (!String.Equals(model.Password, model.ConfirmPassword)) {
                ModelState.AddModelError("", "Пароли не совпадают");
                return View(model);
            }

            var user = new User {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = UserManager.Create(user, model.Password);

            if (result.Succeeded) {
                UserManager.AddToRole(UserManager.FindByName(user.UserName).Id, "users");
                SignIn(user, true);
                return RedirectToAction("index", "home");
            }

            foreach (var error in result.Errors) {
                ModelState.AddModelError("", error);
            }

            return View();
        }
        [Authorize]
        public ActionResult Signout() {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("index", "home");
        }

        private void SignIn(User user, bool isPersistent) {
            var identity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            if (isPersistent) {
                AuthenticationManager.SignIn(new AuthenticationProperties {
                    IsPersistent = true
                }, identity);
            } else {
                AuthenticationManager.SignIn(
                    new AuthenticationProperties {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                    },
                    identity);
            }
        }

        [HttpPost]
        public JsonResult CheckLogin(string username) {
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<User>>();
            return Json(!userManager.Users.Any(u => u.UserName.Equals(username)));
        }
    }
}