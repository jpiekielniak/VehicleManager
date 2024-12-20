namespace VehicleManager.Api.Endpoints.Admin;

public class AdminEndpoints
{
    private static string AdminPath => "admin";
    public static string Admin => "Admin";
    private static string BasePath => $"{RootPath.BasePath}/{AdminPath}";
    public static string BrowseUsers => $"{BasePath}/browse-users";
    public static string SendEmail => $"{BasePath}/send-email";
    public static string DeleteUser => $"{BasePath}/delete-user";
}