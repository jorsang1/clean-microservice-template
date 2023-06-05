using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace CleanCompanyName.DDDMicroservice.Infrastructure.Resilience;

internal static class CircuitBreakerPolicyBuilder
{
    public static IHttpClientBuilder AddCircuitBreakerPolicy(
        this IHttpClientBuilder builder,
        IConfigurationSection configuration)
    {
        builder.AddPolicyHandler(
            HttpPolicyExtensions.HandleTransientHttpError()
                                .AdvancedCircuitBreakerAsync(
                                    failureThreshold: 0.75,
                                    samplingDuration: TimeSpan.FromSeconds(30),
                                    minimumThroughput: 10,
                                    durationOfBreak: TimeSpan.FromSeconds(
                                        configuration.GetValue<int>("CircuitBreakerBreakDuration"))));

        return builder;
    }
}