using Microsoft.AspNet.Identity.EntityFramework;

namespace Infrastructure.Domain {
    public class Role :IdentityRole {
        public Role(string roleName) : base(roleName) {
        }

        public Role() {
        }
    }
}