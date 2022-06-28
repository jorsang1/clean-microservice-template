using System.Diagnostics;

namespace CleanCompanyName.DDDMicroservice.Api.Middleware;

public class RequestScopeLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public RequestScopeLoggingMiddleware(RequestDelegate next, ILogger<RequestScopeLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var scope = context.Request.Path + "-" + Guid.NewGuid();
        var stopwatch = Stopwatch.StartNew();

        using var _ = _logger.BeginScope(scope);
        _logger.LogInformation("⟶ URL {scope} has started", scope);

        try
        {
            await _next(context);
        }
        finally
        {
            _logger.LogInformation("⟵ URL {scope} has finished in {actionDuration}", scope, stopwatch.Elapsed);
        }
    }
}