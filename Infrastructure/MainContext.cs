using Infrastructure.Domain;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Infrastructure
{
    public class MainContext: IdentityDbContext<User> {
        public MainContext()
            : base("defaultconnection") {
        }
    }
}
