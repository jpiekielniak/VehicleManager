using VehicleManager.Api;
using VehicleManager.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
                policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();

                });
});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("test", policy =>
    {
        policy.RequireAuthenticatedUser();
    });
});
builder.Services.LoadLayers(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddlewares();
app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.MapEndpoints();
app.UseHttpsRedirection();

app.Run();


namespace VehicleManager.Api
{
    public partial class Program;
}