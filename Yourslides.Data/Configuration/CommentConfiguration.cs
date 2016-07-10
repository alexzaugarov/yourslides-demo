using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Yourslides.Model;

namespace Yourslides.Data.Configuration {
    public class CommentConfiguration :EntityTypeConfiguration<Comment> {
        public CommentConfiguration() {
            ToTable("Comments");
            HasKey(c => c.Id);
            Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(c => c.Owner)
                .WithMany()
                .HasForeignKey(c => c.OwnerId)
                .WillCascadeOnDelete(false);
            HasRequired(c => c.Presentation)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PresentationId);
            Property(c => c.CreatedDateTime).IsRequired();
            Property(c => c.Text).IsRequired().HasMaxLength(500);
        }
    }
}