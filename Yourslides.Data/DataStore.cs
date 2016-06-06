using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Yourslides.Model;
using Yourslides.Model.Account;

namespace Yourslides.Data {
    public class DataStore : IdentityDbContext<User> {
        public DbSet<Presentation> Presentations { get; set; }
        public DataStore()
            : base("defaultconnection") {
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }
        public virtual void Commit() {
            base.SaveChanges();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.AddFromAssembly(GetType().Assembly);
        }
    }
}
