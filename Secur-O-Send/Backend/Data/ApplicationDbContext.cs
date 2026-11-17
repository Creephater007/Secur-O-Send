namespace Secur_O_Send.Backend.Data;

using Microsoft.EntityFrameworkCore;
using Secur_O_Send.Models;

/// <summary>
/// Database context for Secur-O-Send application.
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Messages table.
    /// </summary>
    public DbSet<Message> Messages { get; set; } = null!;

    /// <summary>
    /// Users table
    /// </summary>
    public DbSet<Message> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Message entity
        modelBuilder.Entity<Message>()
            .HasKey(m => m.Id);

        modelBuilder.Entity<Message>()
            .Property(m => m.Content)
            .IsRequired();
    }
}
