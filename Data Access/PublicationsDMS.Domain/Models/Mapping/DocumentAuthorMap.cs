using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PublicationsDMS.Domain.Models.Mapping
{
    public class DocumentAuthorMap : EntityTypeConfiguration<DocumentAuthor>
    {
        public DocumentAuthorMap()
        {
            // Primary Key
            this.HasKey(t => t.DocumentAuthorID);

            // Properties
            this.Property(t => t.DocumentAuthorID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DocumentAuthor");
            this.Property(t => t.DocumentAuthorID).HasColumnName("DocumentAuthorID");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.AuthorID).HasColumnName("AuthorID");

            // Relationships
            this.HasRequired(t => t.Author)
                .WithMany(t => t.DocumentAuthors)
                .HasForeignKey(d => d.AuthorID);
            this.HasRequired(t => t.Document)
                .WithMany(t => t.DocumentAuthors)
                .HasForeignKey(d => d.DocumentID);

        }
    }
}
