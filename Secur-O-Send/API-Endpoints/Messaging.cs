namespace Secur_O_Send.Backend.API_Endpoints;
using Secur_O_Send.Backend.Attributes;
using Secur_O_Send.Models;

[APIGroup("messages")]
public class Messaging
{
    [APIEndpoint("POST", "/send")]
    public static SendMessageRequest SendMessage(SendMessageRequest request)
    {
        // Log the received message
        Console.WriteLine($"Content received: {request.Message}");

        // Return the same body that was received
        return request;
    }
}
