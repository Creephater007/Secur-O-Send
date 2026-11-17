namespace Secur_O_Send.Backend.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class APIEndpointAttribute : Attribute
{
    public string Method { get; }
    public string Route { get; }

    public APIEndpointAttribute(string method, string route)
    {
        Method = method;
        Route = route;
    }
}