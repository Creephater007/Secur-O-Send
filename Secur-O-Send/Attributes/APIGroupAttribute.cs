namespace Secur_O_Send.Backend.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class APIGroupAttribute : Attribute
{
    public string Route { get; }

    public APIGroupAttribute(string route)
    {
        Route = route;
    }
}
