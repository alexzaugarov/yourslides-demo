﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;

namespace YourSlides.Web.Hubs {
    public class MyIdProvider:IUserIdProvider {
        public string GetUserId(IRequest request) {
            return request.User.Identity.GetUserId();
        }
    }
}