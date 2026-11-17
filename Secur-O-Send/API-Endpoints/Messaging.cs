namespace Secur_O_Send.API_Endpoints;
using Secur_O_Send.Backend.Attributes;

[APIGroup("message")]
public class Messaging
{
    [APIEndpoint("POST", "/send")]
    public static void SendMessage(string message, string recipient)
    {
        // Implement the logic to send a message
        Console.WriteLine($"Message sent to {recipient}: {message}");
    }
}
