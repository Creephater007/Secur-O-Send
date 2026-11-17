namespace Secur_O_Send.Backend.Data;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Helper class for database initialization.
/// </summary>
public static class DatabaseInitializer
{
    /// <summary>
    /// Initializes the database and applies all pending migrations.
    /// </summary>
    public static async Task InitializeAsync(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Create the database if it doesn't exist
            await context.Database.EnsureCreatedAsync();

            Console.WriteLine("✓ Database initialized successfully.");
            Console.WriteLine($"✓ Database location: {GetDatabasePath()}");
        }
    }

    /// <summary>
    /// Gets the full path to the SQLite database file.
    /// </summary>
    private static string GetDatabasePath()
    {
        var projectDirectory = AppContext.BaseDirectory;
        return Path.Combine(projectDirectory, "secur-o-send.db");
    }
}
