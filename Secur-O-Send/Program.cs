using Secur_O_Send.Backend;

var builder = WebApplication.CreateBuilder(args);

// Add frontend CORS policy to allow requests from the frontend application
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .AllowAnyOrigin()
            //.WithOrigins("http://localhost:5062", "http://localhost", "http://localhost:5173", "http://localhost:5174")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

// Only use HTTPS redirection in non-development environments or when HTTPS is configured
if (!app.Environment.IsDevelopment()) app.UseHttpsRedirection();

// Start the CORS middleware before mapping the API endpoints
app.UseCors("Frontend");

app.MapAPIEndpoints("Secur_O_Send.Backend.API_Endpoints");

app.Run();