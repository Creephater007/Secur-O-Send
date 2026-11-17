namespace Secur_O_Send.Backend;

using Secur_O_Send.Backend.Attributes;
using System.Linq.Expressions;
using System.Reflection;

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
                Delegate handler = method.CreateDelegate(Expression.GetDelegateType(method.GetParameters().Select(p => p.ParameterType).Append(method.ReturnType).ToArray()));
                string route = $"/api/{group.Route.Trim('/')}/{endpoint.Route.TrimStart('/')}";

                app.MapMethods(route, new[] { endpoint.Method }, handler);
            }
        }
    }
}