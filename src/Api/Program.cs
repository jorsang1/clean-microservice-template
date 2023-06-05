using System.Text.Json;
using System.Text.Json.Serialization;
using CleanCompanyName.DDDMicroservice.Api;
using CleanCompanyName.DDDMicroservice.Application;
using CleanCompanyName.DDDMicroservice.Domain;
using CleanCompanyName.DDDMicroservice.Infrastructure;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

AddLoggingFromConfiguration(builder);

// Add services to the container.
builder.Services.AddApi();
builder.Services.AddApplication();
builder.Services.AddDomain();
builder.Services.AddInfrastructure(builder.Configuration.GetSection("Infrastructure"));

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});


var app = builder.Build();

app.Logger.LogInformation("Starting CleanMicroserviceTemplate...");

// Configure the HTTP request pipeline.
app.UseApi();
app.UseHttpsRedirection();
app.Run();

static void AddLoggingFromConfiguration(WebApplicationBuilder builder)
{
    var isJsonConsoleLoggingEnabled = builder.Configuration.GetValue<bool>("Logging:JsonConsoleLoggingEnabled");
    builder.Logging.ClearProviders();
    if (isJsonConsoleLoggingEnabled)
    {
        builder.Logging.AddJsonConsole(
            options =>
            {
                options.IncludeScopes     = true;
                options.TimestampFormat   = "hh:mm:ss";
                options.JsonWriterOptions = new JsonWriterOptions { Indented = false };
            });
    }
    else
        builder.Logging.AddConsole();
}