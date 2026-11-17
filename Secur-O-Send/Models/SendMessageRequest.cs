namespace Secur_O_Send.Models;

/// <summary>
/// Request model for sending a message.
/// </summary>
public class SendMessageRequest
{
    /// <summary>
    /// The message content to send.
    /// </summary>
    public string Message { get; set; } = string.Empty;
}