using System.Diagnostics;

namespace CleanCompanyName.DDDMicroservice.Application.Pipelines;

internal class TracingEnrichingPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        using var activity = Activity.Current?.Source.StartActivity($"Application handler: {typeof(TRequest).Name}");

        return await next();
    }
}