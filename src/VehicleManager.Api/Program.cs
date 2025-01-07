using VehicleManager.Api;
using VehicleManager.Api.Configuration.Cors;
using VehicleManager.Api.Configuration.FormFile;
using VehicleManager.Api.Configuration.Swagger;
using VehicleManager.Infrastructure.Common.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSwagger()
    .AddCorsPolicy()
    .AddAuthorization()
    .AddEndpointsApiExplorer()
    .AddFormFileConfiguration()
    .AddJsonMultipartFormDataSupport(JsonSerializerChoice.Newtonsoft)
    .LoadLayers(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles()
    .UseMiddlewares()
    .UseCors("AllowAllOrigins")
    .UseAuthentication()
    .UseAuthorization()
    .UseHttpsRedirection();

app.MapEndpoints();
app.Run();

namespace VehicleManager.Api
{
    public partial class Program;
}