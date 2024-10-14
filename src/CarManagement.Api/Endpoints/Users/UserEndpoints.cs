namespace CarManagement.Api.Endpoints.Users;

public static class UserEndpoints
{
    public static string Users => "Users";
    public static string SignUp => $"{BasePath}/sign-up";
    public static string SignIn => $"{BasePath}/sign-in";
    private static string BasePath => $"{RootPath.BasePath}/{Users.ToLowerInvariant()}";
}