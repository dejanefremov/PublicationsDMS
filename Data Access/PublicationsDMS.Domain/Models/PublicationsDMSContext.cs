using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using PublicationsDMS.Domain.Models.Mapping;

namespace PublicationsDMS.Domain.Models
{
    public partial class PublicationsDMSContext : DbContext
    {
        static PublicationsDMSContext()
        {
            Database.SetInitializer<PublicationsDMSContext>(null);
        }

        public PublicationsDMSContext()
            : base("Name=PublicationsDMSContext")
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<DataItem> DataItems { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentAuthor> DocumentAuthors { get; set; }
        public DbSet<DocumentTag> DocumentTags { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDataItemPermission> UserDataItemPermissions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AuthorMap());
            modelBuilder.Configurations.Add(new DataItemMap());
            modelBuilder.Configurations.Add(new DocumentMap());
            modelBuilder.Configurations.Add(new DocumentAuthorMap());
            modelBuilder.Configurations.Add(new DocumentTagMap());
            modelBuilder.Configurations.Add(new FolderMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new TagMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserDataItemPermissionMap());
        }
    }
}
