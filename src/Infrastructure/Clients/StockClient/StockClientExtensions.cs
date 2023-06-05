using System.Net.Http.Headers;
using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Infrastructure.Resilience;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanCompanyName.DDDMicroservice.Infrastructure.Clients.StockClient;

internal static class StockClientExtensions
{
    public static IServiceCollection AddStockClientConfiguration(
        this IServiceCollection services,
        IConfigurationSection configuration)
    {
        var baseUrl = configuration.GetValue<string>("StockClientConfiguration:BaseUrl");
        var secret = configuration.GetValue<string>("StockClientConfiguration:Secret");
        var resiliencyConfigSection = configuration.GetSection("HttpClientsResiliency");

        if (string.IsNullOrEmpty(baseUrl))
            throw new ArgumentNullException(nameof(baseUrl), "Base url for the stock client not provided in the configuration.");

        services.AddHttpClient<IStockClient, StockClient>(
                client =>
                {
                    client.BaseAddress = new Uri(baseUrl).EnsureSlashed();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("AccessKey", secret);
                })
            .AddRetryPolicy(resiliencyConfigSection)
            .AddCircuitBreakerPolicy(resiliencyConfigSection);

        return services;
    }
}