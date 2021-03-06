using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Yourslides.Model.Account;

namespace Yourslides.Data.Migrations {
    internal sealed class Configuration : DbMigrationsConfiguration<DataStore> {
        public Configuration() {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataStore context) {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var roles = new List<IdentityRole> {
                new IdentityRole("Admins"),
                new IdentityRole("Users")
            };
            var rolesToAdd = roles.Where(r => !context.Roles.Any(rC => rC.Name == r.Name));
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);
            foreach (var role in rolesToAdd) {
                manager.Create(role);
            }
            if (!(context.Users.Any(u => u.UserName == "Admin"))) {
                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var userToInsert = new User { UserName = "Admin", Email = "alex52711@yandex.ru" };
                userManager.Create(userToInsert, "administratorpassword");
                userManager.AddToRole(userToInsert.Id, "Admins");
                userManager.AddToRole(userToInsert.Id, "Users");
            }
        }
    }
}
