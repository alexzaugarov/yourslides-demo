using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Yourslides.Model;

namespace Yourslides.Data.Configuration {
    public class PresentationConfiguration : EntityTypeConfiguration<Presentation> {
        public PresentationConfiguration() {
            ToTable("Presentation");
            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(p => p.Owner)
                .WithMany(u => u.Presentations)
                .HasForeignKey(p => p.OwnerId);
            Property(p => p.Title).IsRequired().HasMaxLength(100);
            Property(p => p.Description).IsOptional().HasMaxLength(1000);
            Property(p => p.Visibility).IsRequired();
            Property(p => p.ScreenBackgroundColor).IsRequired();
            Property(p => p.ScreenBackgroundColor).HasColumnName("Color");
        }
    }
}