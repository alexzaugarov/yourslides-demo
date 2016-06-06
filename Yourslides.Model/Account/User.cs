using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Yourslides.Model.Account {
    public class User : IdentityUser{
        public List<Presentation> Presentations { get; set; }
    }
}