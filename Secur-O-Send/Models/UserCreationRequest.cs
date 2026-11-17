namespace Secur_O_Send.Models;

/// <summary>
/// Request model for creating a new user.
/// </summary>
public class UserCreationRequest
{

    /// <summary>
    /// Username you want to set for the new user.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Hashed password for the new user.
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    public bool UsernamePublic { get; set; } = false;
}
