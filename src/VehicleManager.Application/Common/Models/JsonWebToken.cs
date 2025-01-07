namespace VehicleManager.Application.Common.Models;

public class JsonWebToken
{
    public string AccessToken { get; set; }
    public DateTime Expires { get; set; }
    public string Id { get; set; }
    public string Role { get; set; }
}