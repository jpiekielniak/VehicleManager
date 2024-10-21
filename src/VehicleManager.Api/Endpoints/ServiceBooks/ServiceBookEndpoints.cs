namespace VehicleManager.Api.Endpoints.ServiceBooks;

public class ServiceBookEndpoints
{
    public static string ServiceBooks => "ServiceBooks";
    private static string ServiceBooksPath => "service-books";
    public static string BasePath => $"{RootPath.BasePath}/{ServiceBooksPath}";
    public static string Inspections => $"{BasePath}/{{serviceBookId:guid}}/inspections";
    public static string Inspection => $"{Inspections}/{{inspectionId:guid}}";
    public static string Services => $"{BasePath}/{{serviceBookId:guid}}/services";
    public static string Service => $"{Services}/{{serviceId:guid}}";
}