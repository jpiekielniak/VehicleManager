namespace VehicleManager.Shared.Auth.Context;

internal class Context : IContext
{
    public Guid Id { get; }

    public Context(IHttpContextAccessor httpContextAccessor)
    {
        var identity = httpContextAccessor.HttpContext?.User.Identity;
        Id = Guid.Parse(identity.Name);
    }
}