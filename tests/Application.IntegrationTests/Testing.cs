using CleanCompanyName.DDDMicroservice.Domain;
using CleanCompanyName.DDDMicroservice.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanCompanyName.DDDMicroservice.Application.IntegrationTests;

public class Testing
{
    // protected IServiceScopeFactory ScopeFactory = null!;
    public IServiceScopeFactory ScopeFactory
    {
        get;
    }

    public Testing()
    {
        var builder = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true);

        var configuration = builder.Build();

        var services = ConfigureServices(configuration);

        ScopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();
    }

    private ServiceCollection ConfigureServices(IConfiguration configuration)
    {
        var services = new ServiceCollection();

        services
            .AddApplication()
            .AddDomain()
            .AddInfrastructure(configuration.GetSection("Infrastructure"));

        services
            .AddLogging();

        return services;
    }
}