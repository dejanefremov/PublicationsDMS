using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PublicationsDMS.Domain.Models.Mapping
{
    public class DataItemMap : EntityTypeConfiguration<DataItem>
    {
        public DataItemMap()
        {
            // Primary Key
            this.HasKey(t => t.DataItemID);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("DataItem");
            this.Property(t => t.DataItemID).HasColumnName("DataItemID");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.ParentFolderID).HasColumnName("ParentFolderID");
            this.Property(t => t.Type).HasColumnName("Type");

            // Relationships
            this.HasOptional(t => t.DataItem2)
                .WithMany(t => t.DataItem1)
                .HasForeignKey(d => d.ParentFolderID);

        }
    }
}
