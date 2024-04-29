using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Polly.CircuitBreaker;

namespace CleanCompanyName.DDDMicroservice.Api.Middleware;

public class ErrorHandlerMiddleware(
    RequestDelegate next,
    ILogger<ErrorHandlerMiddleware> logger,
    IConfiguration configuration)
{
    private const string CircuitBreakerBreakDuration =
        "Infrastructure:HttpClientsResiliency:CircuitBreakerBreakDuration";

    private readonly ILogger _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, configuration, ex, _logger);
        }
    }

    private static Task HandleExceptionAsync(
        HttpContext context,
        IConfiguration configuration,
        Exception exception,
        ILogger logger)
    {
        logger.LogError(exception, "Handling exception in the middleware. Message: {ErrorMessage}", exception.Message);

        var errorDetails = GetErrorDetails(configuration, exception);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode  = errorDetails.Status!.Value;

        var jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        return context.Response.WriteAsync(JsonSerializer.Serialize(errorDetails, jsonSerializerOptions));
    }


    private static ProblemDetails GetErrorDetails(IConfiguration configuration, Exception exception)
        => exception is BrokenCircuitException
            ? GetServiceUnavailableErrorDetails(configuration)
            : GetGenericInternalErrorDetails();

    private static ProblemDetails GetGenericInternalErrorDetails()
        => new()
        {
            Title = "Something went wrong processing your request",
            Detail =
                "Try again later and if the problem persist contact LanguageWire support. Please try to provide as much information as you can such as: datetime of the request, the endpoint, the request body, etc.",
            Status = (int)HttpStatusCode.InternalServerError
        };

    private static ProblemDetails GetServiceUnavailableErrorDetails(IConfiguration configuration)
        => new()
        {
            Title = "Service unavailable at the moment.",
            Detail =
                $"Please wait {configuration.GetValue<int>(CircuitBreakerBreakDuration)} seconds before next call",
            Status = (int)HttpStatusCode.ServiceUnavailable
        };
}