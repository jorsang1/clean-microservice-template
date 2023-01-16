using System.Diagnostics;

namespace CleanCompanyName.DDDMicroservice.Application.Pipelines;

internal class ExceptionEnrichingPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            Activity.Current?.AddTag("Error", true);
            ex.Data["ActionName"] = typeof(TRequest).Name;
            throw;
        }
    }
}