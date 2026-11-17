namespace Secur_O_Send.Web;


public static class Constants
{
    public const string BackendIpAddress = "localhost";
    public const int BackendPort = 5106;
    public static string BackendSocket => $"http://{BackendIpAddress}:{BackendPort}";
    public static string BackendApiRoute => $"{BackendSocket}/api";
}