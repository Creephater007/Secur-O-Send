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
    public static async Task SendMessage(SendMessageRequest request, ApplicationDbContext dbContext, IHeaderDictionary headers)
    {
        // Log the received message
        Console.WriteLine($"Message received from {headers["Authorization"]} to {request.Destination}");

        // Speichere die Nachricht in der Datenbank
        var message = new Message
        { 
            Content = request.Content,
            Source = dbContext.GetUserByToken(headers["Authorization"].ToString()).Id,
            Destination = dbContext.GetUserByToken(request.Destination).Id
        };
        dbContext.Messages.Add(message);
        await dbContext.SaveChangesAsync();
    }

    [APIEndpoint("GET", "/get-new")]
    public static async Task<IEnumerable<Message>> GetNewMessages(ApplicationDbContext dbContext, IHeaderDictionary headers)
    {
        var user = dbContext.GetUserByToken(headers["Authorization"].ToString());

        var allMsgs = await dbContext.Messages.Where(m => m.Destination == user.Id).ToListAsync();

        return allMsgs;
    }
}
