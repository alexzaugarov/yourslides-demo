using System.Data.Entity.ModelConfiguration;
using Yourslides.Model;

namespace Yourslides.Data.Configuration {
    public class PresentationWatchConfiguration : EntityTypeConfiguration<PresentationWatch> {
        public PresentationWatchConfiguration() {
            HasKey(w => new { w.PresentationId, w.VisitorId });
            HasRequired(w => w.Presentation)
                .WithMany()
                .HasForeignKey(w => w.PresentationId);
        }
    }
}