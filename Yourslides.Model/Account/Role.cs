using Microsoft.AspNet.Identity.EntityFramework;

namespace Yourslides.Model.Account {
    public class Role :IdentityRole {
        public Role(string roleName) : base(roleName) {
        }

        public Role() {
        }
    }
}