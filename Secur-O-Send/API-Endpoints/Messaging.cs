namespace Secur_O_Send.Backend.API_Endpoints;

using Secur_O_Send.Backend.Attributes;
using Secur_O_Send.Models;
using Secur_O_Send.Backend.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[APIGroup("messages")]
public class Messaging
{
    [APIEndpoint("POST", "/send")]
    public static async Task SendMessage(SendMessageRequest request, ApplicationDbContext dbContext)
    {
        // Log the received message
        Console.WriteLine($"Message received: {request.Message}");

        // Speichere die Nachricht in der Datenbank
        var message = new Message { Content = request.Message };
        dbContext.Messages.Add(message);
        await dbContext.SaveChangesAsync();
    }

    [APIEndpoint("GET", "/get-new")]
    public static async Task<IEnumerable<Message>> GetNewMessages(ApplicationDbContext dbContext)
    {
        var allMsgs = await dbContext.Messages.Where(m => 1 == 1).ToListAsync();

        // Return the same body that was received
        return allMsgs;
    }
}
