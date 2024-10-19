namespace CarManagement.Api.Endpoints.ServiceBooks;

public class ServiceBookEndpoints
{
    public static string ServiceBooks => "ServiceBooks";
    public static string BasePath => $"{RootPath.BasePath}/{ServiceBooks.ToLowerInvariant()}";
    public static string Inspections => $"{BasePath}/{{serviceBookId:guid}}/inspections";
}