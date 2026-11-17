namespace Secur_O_Send.Backend;

using Secur_O_Send.Backend.Attributes;
using System.Reflection;
using System.Text.Json;

public static class APIEndpointExtensions
{
    public static void MapAPIEndpoints(this WebApplication app, string namespaceName)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        foreach (Type type in assembly.GetTypes())
        {
            if (type.Namespace != namespaceName) continue;

            APIGroupAttribute? group = type.GetCustomAttribute<APIGroupAttribute>();

            if (group == null) continue;

            foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly))
            {
                APIEndpointAttribute? endpoint = method.GetCustomAttribute<APIEndpointAttribute>();
                if (endpoint == null) continue;

                string route = $"/api/{group.Route.Trim('/')}/{endpoint.Route.TrimStart('/')}";

                // Create a RequestDelegate that wraps the endpoint method
                RequestDelegate requestDelegate = CreateRequestDelegate(method);

                app.MapMethods(route, new[] { endpoint.Method }, requestDelegate);
            }
        }
    }

    private static RequestDelegate CreateRequestDelegate(MethodInfo method)
    {
        return async context =>
        {
            try
            {
                // Enable request body buffering
                context.Request.EnableBuffering();

                // Get method parameters
                var parameters = method.GetParameters();
                var paramValues = new object?[parameters.Length];

                // Populate parameters
                for (int i = 0; i < parameters.Length; i++)
                {
                    var paramType = parameters[i].ParameterType;

                    // If parameter is HttpContext, pass it directly
                    if (paramType == typeof(HttpContext))
                    {
                        paramValues[i] = context;
                    }
                    // Otherwise, try to deserialize from JSON body
                    else
                    {
                        context.Request.Body.Position = 0;
                        using (StreamReader reader = new StreamReader(context.Request.Body))
                        {
                            string body = await reader.ReadToEndAsync();

                            // Try to deserialize to the parameter type
                            paramValues[i] = string.IsNullOrEmpty(body) 
                                ? null 
                                : JsonSerializer.Deserialize(body, paramType, new JsonSerializerOptions 
                                { 
                                    PropertyNameCaseInsensitive = true 
                                });
                        }
                    }
                }

                // Invoke the method
                var result = method.Invoke(null, paramValues);

                // Handle Task results
                if (result is Task task)
                {
                    await task;

                    // If it's a Task<T>, get the result
                    var resultProperty = task.GetType().GetProperty("Result");
                    if (resultProperty != null)
                    {
                        result = resultProperty.GetValue(task);
                    }
                    else
                    {
                        result = null;
                    }
                }

                // Write response
                if (result != null)
                {
                    await context.Response.WriteAsJsonAsync(result);
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            }
        };
    }
}