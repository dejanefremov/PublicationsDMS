using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PublicationsDMS.Domain.Models.Mapping
{
    public class DocumentTagMap : EntityTypeConfiguration<DocumentTag>
    {
        public DocumentTagMap()
        {
            // Primary Key
            this.HasKey(t => t.DocumentTagID);

            // Properties
            this.Property(t => t.DocumentTagID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DocumentTag");
            this.Property(t => t.DocumentTagID).HasColumnName("DocumentTagID");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.TagID).HasColumnName("TagID");

            // Relationships
            this.HasRequired(t => t.Document)
                .WithMany(t => t.DocumentTags)
                .HasForeignKey(d => d.DocumentID);
            this.HasRequired(t => t.Tag)
                .WithMany(t => t.DocumentTags)
                .HasForeignKey(d => d.TagID);

        }
    }
}
