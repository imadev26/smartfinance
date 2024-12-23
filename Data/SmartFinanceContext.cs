using Microsoft.EntityFrameworkCore;
using SmartFinanceAPI.Models;

namespace SmartFinanceAPI.Data;

public class SmartFinanceContext : DbContext
{
    public SmartFinanceContext(DbContextOptions<SmartFinanceContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure table names to match database
        modelBuilder.Entity<Budget>().ToTable("budget");
        modelBuilder.Entity<Category>().ToTable("category");
        modelBuilder.Entity<Goal>().ToTable("goal");
        modelBuilder.Entity<Notification>().ToTable("notification");
        modelBuilder.Entity<Report>().ToTable("report");
        modelBuilder.Entity<Transaction>().ToTable("transaction");
        modelBuilder.Entity<User>().ToTable("user");
        modelBuilder.Entity<UserSettings>().ToTable("usersettings");

        // Configure relationships
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Category)
            .WithMany(c => c.Transactions)
            .HasForeignKey(t => t.CategoryId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.UserSettings)
            .WithOne(us => us.User)
            .HasForeignKey<UserSettings>(us => us.UserId);

        // Add other configurations...
    }

    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserSettings> UserSettings { get; set; }
} 