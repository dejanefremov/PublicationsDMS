using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PublicationsDMS.Domain.Models.Mapping
{
    public class DocumentMap : EntityTypeConfiguration<Document>
    {
        public DocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.DocumentID);

            // Properties
            this.Property(t => t.DocumentID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FileExtension)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Document");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.FileID).HasColumnName("FileID");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.FileExtension).HasColumnName("FileExtension");

            // Relationships
            this.HasRequired(t => t.DataItem)
                .WithOptional(t => t.Document);

        }
    }
}
