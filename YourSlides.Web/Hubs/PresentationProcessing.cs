using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace YourSlides.Web.Hubs {
    [Authorize]
    public class PresentationProcessing : Hub {
        public void Hello() {
            
        }
    }
}