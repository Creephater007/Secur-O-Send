using Microsoft.EntityFrameworkCore;
using Secur_O_Send.Backend.Attributes;
using Secur_O_Send.Backend.Data;
using Secur_O_Send.Models;


namespace Secur_O_Send.API_Endpoints;

[APIGroup("users")]
public class UserEndpoints
{
    [APIEndpoint("POST", "/create")]
    public static async Task CreateUser(UserCreationRequest request, ApplicationDbContext dbContext)
    {
        Console.WriteLine($"Creating user: {request.Username}, Public: {request.UsernamePublic}");

        if (await dbContext.Users.AnyAsync(u => u.Username == request.Username))
        {
            Console.WriteLine($"User already exists: {request.Username}");
            throw new InvalidOperationException("Username already exists.");
        }

        var user = new User
        {
            Username = request.Username,
            PasswordHash = request.PasswordHash,
            UsernamePublic = request.UsernamePublic
        };
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();

        Console.WriteLine($"User created successfully: {request.Username}");
    }
}
