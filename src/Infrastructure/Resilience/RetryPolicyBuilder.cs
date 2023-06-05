using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace CleanCompanyName.DDDMicroservice.Infrastructure.Resilience;

internal static class RetryPolicyBuilder
{
    public static IHttpClientBuilder AddRetryPolicy(
        this IHttpClientBuilder builder,
        IConfigurationSection configuration)
    {
        var retryTimesList = configuration.GetValue<string>("RetriesTimes")!.Split(',');

        builder.AddTransientHttpErrorPolicy(
            policyBuilder => policyBuilder.WaitAndRetryAsync(
                retryTimesList.Select(retryTime => TimeSpan.FromSeconds(float.Parse(retryTime)))));

        return builder;
    }

    internal static IHttpClientBuilder AddRetryPolicyForNotTooBigRequests(
        this IHttpClientBuilder builder,
        IConfiguration configuration)
    {
        var retryTimesList          = configuration.GetValue<string>("RetriesTimes")!.Split(',');
        var retriesSizeLimitInMb    = configuration.GetValue<long>("RetriesSizeLimitInMB");
        var retriesSizeLimitInBytes = retriesSizeLimitInMb * 1024L * 1024L;

        var policy = Policy.Handle<HttpRequestException>()
                           .OrResult<HttpResponseMessage>(
                               response => IsInternalServerErrorAndFileIsNotTooBig(response, retriesSizeLimitInBytes))
                           .WaitAndRetryAsync(
                               retryTimesList.Select(retryTime => TimeSpan.FromSeconds(float.Parse(retryTime))));

        builder.AddPolicyHandler(policy);

        return builder;
    }

    private static bool IsInternalServerErrorAndFileIsNotTooBig(HttpResponseMessage response, long limitToRetry)
        => response.StatusCode >= HttpStatusCode.InternalServerError
           && !RequestedFileIsTooBig(response.RequestMessage, limitToRetry);

    private static bool RequestedFileIsTooBig(HttpRequestMessage? request, long limitToRetry)
    {
        if (request?.Content is not MultipartContent)
            return false;

        return request.Content?.Headers.ContentLength > limitToRetry;
    }
}