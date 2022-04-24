using Api;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration.GetSection("Infrastructure"));

var app = builder.Build();

app.Logger.LogInformation("Starting CleanMicroserviceTemplate...");

// Configure the HTTP request pipeline.
app.UseApi();
app.UseHttpsRedirection();
app.Run();