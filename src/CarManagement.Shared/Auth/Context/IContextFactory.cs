namespace CarManagement.Shared.Auth.Context;

internal interface IContextFactory
{
    IContext Create();
}