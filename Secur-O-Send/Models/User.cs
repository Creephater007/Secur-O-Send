namespace Secur_O_Send.Models;

/// <summary>
/// User entity for database storage.
/// </summary>
public class User
{
    /// <summary>
    /// Unique identifier for the user.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// The username of the user.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// The password hash of the user.
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    public bool UsernamePublic { get; set; } = false;
}
