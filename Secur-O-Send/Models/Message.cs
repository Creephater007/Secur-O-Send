namespace Secur_O_Send.Models;

/// <summary>
/// Message entity for database storage.
/// </summary>
public class Message
{
    /// <summary>
    /// Unique identifier for the message.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The message content.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp when the message was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid Source { get; set; } = Guid.Empty;
    public Guid Destination { get; set; } = Guid.Empty;
}