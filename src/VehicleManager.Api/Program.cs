using Microsoft.AspNetCore.Http.Features;
using Swashbuckle.AspNetCore.JsonMultipartFormDataSupport.Extensions;
using Swashbuckle.AspNetCore.JsonMultipartFormDataSupport.Integrations;
using VehicleManager.Api;
using VehicleManager.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddJsonMultipartFormDataSupport(JsonSerializerChoice.Newtonsoft);
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
    options.AddPolicy("test", policy => { policy.RequireAuthenticatedUser(); });
});
builder.Services.LoadLayers(builder.Configuration);

builder.Services.Configure<FormOptions>(options => { options.MultipartBodyLengthLimit = 5 * 1024 * 1024; });


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
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