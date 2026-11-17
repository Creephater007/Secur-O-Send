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
    /// Used to retrieve user information by their token
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public User GetUserByToken(string token)
    {
        // Implement your logic to retrieve the user based on the provided token.
        // This is a placeholder implementation; you should replace it with your actual logic.
        return Users.FirstOrDefault(u => u.Username == token) ?? throw new InvalidOperationException("User not found");
    }

    /// <summary>
    /// Used to retrieve user information by a public ID in form of a conversational ID or username.
    /// </summary>
    /// <param name="pId"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public User GetUserByPublicId(string pId)
    {
        // Implement your logic to retrieve the user based on the provided public ID.
        // This is a placeholder implementation; you should replace it with your actual logic.
        return Users.FirstOrDefault(u => u.Id.ToString() == pId) ?? throw new InvalidOperationException("User not found");
    }

    /// <summary>
    /// Messages table
    /// </summary>
    public DbSet<Message> Messages { get; set; } = null!;

    /// <summary>
    /// Users table
    /// </summary>
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Message entity
        modelBuilder.Entity<Message>()
            .HasKey(m => m.Id);

        modelBuilder.Entity<Message>()
            .Property(m => m.Content)
            .IsRequired();

        modelBuilder.Entity<Message>()
            .Property(m => m.Source)
            .IsRequired();

        modelBuilder.Entity<Message>()
            .Property(m => m.Destination)
            .IsRequired();


        // Configure User entity
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<User>()
            .Property(u => u.Username)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.PasswordHash)
            .IsRequired();
    }
}
