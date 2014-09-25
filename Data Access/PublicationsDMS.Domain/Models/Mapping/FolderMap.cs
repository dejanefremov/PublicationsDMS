using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PublicationsDMS.Domain.Models.Mapping
{
    public class FolderMap : EntityTypeConfiguration<Folder>
    {
        public FolderMap()
        {
            // Primary Key
            this.HasKey(t => t.FolderID);

            // Properties
            this.Property(t => t.FolderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Folder");
            this.Property(t => t.FolderID).HasColumnName("FolderID");

            // Relationships
            this.HasRequired(t => t.DataItem)
                .WithOptional(t => t.Folder);

        }
    }
}
