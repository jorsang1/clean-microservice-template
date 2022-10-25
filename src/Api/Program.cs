using System.Text.Json;
using System.Text.Json.Serialization;
using CleanCompanyName.DDDMicroservice.Api;
using CleanCompanyName.DDDMicroservice.Application;
using CleanCompanyName.DDDMicroservice.Domain;
using CleanCompanyName.DDDMicroservice.Infrastructure;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddApi();
builder.Services.AddApplication();
builder.Services.AddDomain();
builder.Services.AddInfrastructure(builder.Configuration.GetSection("Infrastructure"));

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});


var app = builder.Build();

app.Logger.LogInformation("Starting CleanMicroserviceTemplate...");

// Configure the HTTP request pipeline.
app.UseApi();
app.UseHttpsRedirection();
app.Run();