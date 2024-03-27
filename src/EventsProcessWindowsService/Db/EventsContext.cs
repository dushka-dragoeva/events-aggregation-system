using EventsProcessWindowsService.Db.DataObjects;
using System.Data.Entity;
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
        public DbSet<UserLoginEvent> UserLogins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}