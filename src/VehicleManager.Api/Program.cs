using VehicleManager.Api;
using VehicleManager.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.LoadLayers(builder.Configuration);

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddlewares();
app.MapEndpoints();
app.UseHttpsRedirection();

app.Run();


namespace VehicleManager.Api
{
    public partial class Program;
}