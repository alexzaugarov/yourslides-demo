using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using Yourslides.Model;
using Yourslides.Model.Account;

namespace Yourslides.Data {
    public class DataStore : IdentityDbContext<User> {
        public DbSet<Presentation> Presentations { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PresentationWatch> PresentationWatches{ get; set; }
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
