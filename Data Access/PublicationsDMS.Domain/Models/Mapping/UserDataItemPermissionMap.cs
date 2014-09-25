using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PublicationsDMS.Domain.Models.Mapping
{
    public class UserDataItemPermissionMap : EntityTypeConfiguration<UserDataItemPermission>
    {
        public UserDataItemPermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.UserDataItemPermissionID);

            // Properties
            // Table & Column Mappings
            this.ToTable("UserDataItemPermission");
            this.Property(t => t.UserDataItemPermissionID).HasColumnName("UserDataItemPermissionID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.DataItemID).HasColumnName("DataItemID");

            // Relationships
            this.HasRequired(t => t.DataItem)
                .WithMany(t => t.UserDataItemPermissions)
                .HasForeignKey(d => d.DataItemID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserDataItemPermissions)
                .HasForeignKey(d => d.UserID);

        }
    }
}
