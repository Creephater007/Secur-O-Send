namespace Secur_O_Send.Models;

/// <summary>
/// Request model for sending a message.
/// </summary>
public class SendMessageRequest
{
    /// <summary>
    /// The message content to send.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Username or conversation ID of the receiving person.
    /// </summary>
    public string Destination { get; set; } = string.Empty;

    /// <summary>
    /// Token of the sender for authentication.
    /// </summary>
    public string SourceToken { get; set; } = string.Empty;
}