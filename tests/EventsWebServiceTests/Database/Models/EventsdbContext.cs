using Microsoft.EntityFrameworkCore;

namespace EventsWebServiceTests.Database.Models;

public partial class EventsdbContext : DbContext
{
    private readonly string _connectionString;

    public EventsdbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public EventsdbContext()
    {
    }

    public EventsdbContext(DbContextOptions<EventsdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FileDownloadEvent> FileDownloadEvents { get; set; }

    public virtual DbSet<ProductActionTraking> ProductActionTrakings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLogOutEvent> UserLogOutEvents { get; set; }

    public virtual DbSet<UserLoginEvent> UserLoginEvents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FileDownloadEvent>(entity =>
        {
            entity.ToTable("FileDownloadEvent");
        });

        modelBuilder.Entity<ProductActionTraking>(entity =>
        {
            entity.ToTable("ProductActionTraking");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
        });

        modelBuilder.Entity<UserLogOutEvent>(entity =>
        {
            entity.ToTable("UserLogOutEvent");
        });

        modelBuilder.Entity<UserLoginEvent>(entity =>
        {
            entity.ToTable("UserLoginEvent");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
