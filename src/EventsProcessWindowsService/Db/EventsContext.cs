using EventsProcessWindowsService.Db.DataObjects;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EventsProcessWindowsService.Db
{
    public class EventsContext : DbContext
    {
        public EventsContext()
        {
            Database.SetInitializer<EventsContext>(null);
        }

        public DbSet<FileDownloadEvent> FileDownloads { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //EntityTypeConfiguration<FileDownloadEvent> storedFileDownloadData = modelBuilder.Entity<FileDownloadEvent>();

            //storedFileDownloadData.HasKey(x => x.Id);
            //storedFileDownloadData.Property(x => x.EventId).IsRequired();
            //storedFileDownloadData.Property(x => x.Date);
            //storedFileDownloadData.Property(x => x.FileName);
            //storedFileDownloadData.Property(x => x.FileLenght);

            base.OnModelCreating(modelBuilder);
        }
    }
}