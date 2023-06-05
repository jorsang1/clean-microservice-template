using System.Net;
using CleanCompanyName.DDDMicroservice.Infrastructure.IntegrationTests.PollyRetrialsTests;
using FluentAssertions;
using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Infrastructure.Clients.StockClient;
using CleanCompanyName.DDDMicroservice.Infrastructure.Resilience;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanCompanyName.DDDMicroservice.Infrastructure.IntegrationTests.PollyRetrialsTests;

public class RetryPolicyTest
{
    private const string RetriesSeconds = "1,2,3";
    private HttpMessageHandlerWithRetries _messageHandler = null!;

    [Theory]
    [InlineData(HttpStatusCode.OK)]
    [InlineData(HttpStatusCode.Accepted)]
    [InlineData(HttpStatusCode.NoContent)]
    public async Task WHEN_success_response_is_received_THEN_no_retrials_are_made(HttpStatusCode statusCode)
    {
        _messageHandler = new HttpMessageHandlerWithRetries(statusCode);
        var testApiClient = SetupClientAsInDependencyInjection(_messageHandler);

        await testApiClient.UpdateStock(productId: Guid.NewGuid(), unitsChange: 2);

        _messageHandler.NumberOfRequests.Should().Be(1);
    }

    [Fact]
    public async Task WHEN_it_crashes_THEN_retries_are_made()
    {
        _messageHandler = new HttpMessageHandlerWithRetries(null, null, null, HttpStatusCode.OK);
        var testApiClient = SetupClientAsInDependencyInjection(_messageHandler);

        await testApiClient.UpdateStock(productId: Guid.NewGuid(), unitsChange: 2);

        _messageHandler.NumberOfRequests.Should().Be(4);
    }

    [Theory]
    [InlineData(HttpStatusCode.BadRequest)]
    [InlineData(HttpStatusCode.NotFound)]
    [InlineData(HttpStatusCode.RequestTimeout)]
    [InlineData(HttpStatusCode.TooManyRequests)]
    public async Task WHEN_no_transient_errors_THEN_no_retrials_are_made(HttpStatusCode? statusCode)
    {
        _messageHandler = new HttpMessageHandlerWithRetries(statusCode, statusCode, statusCode, statusCode);
        var testApiClient = SetupClientAsInDependencyInjection(_messageHandler);

        await testApiClient.UpdateStock(productId: Guid.NewGuid(), unitsChange: 2);

        _messageHandler.NumberOfRequests.Should().Be(1);
    }

    [Theory]
    [InlineData(HttpStatusCode.GatewayTimeout)]
    [InlineData(HttpStatusCode.InternalServerError)]
    public async Task WHEN_transient_errors_THEN_regular_retry_policy_applies(HttpStatusCode statusCode)
    {
        _messageHandler = new HttpMessageHandlerWithRetries(statusCode, statusCode, statusCode, statusCode);
        var testApiClient = SetupClientAsInDependencyInjection(_messageHandler);

        await testApiClient.UpdateStock(productId: Guid.NewGuid(), unitsChange: 2);

        _messageHandler.NumberOfRequests.Should().Be(4);
        AssertRetryTimeBetween(_messageHandler.RequestTimes, requestIndex: 2, lowerLimit: 800, upperLimit: 1200);
        AssertRetryTimeBetween(_messageHandler.RequestTimes, requestIndex: 3, lowerLimit: 1800, upperLimit: 2200);
        AssertRetryTimeBetween(_messageHandler.RequestTimes, requestIndex: 4, lowerLimit: 2800, upperLimit: 3200);
    }

    [Fact]
    public async Task WHEN_transient_errors_and_then_ok_THEN_regular_policy_applies_until_ok()
    {
        _messageHandler = new HttpMessageHandlerWithRetries(
            HttpStatusCode.InternalServerError,
            HttpStatusCode.InternalServerError,
            HttpStatusCode.OK);
        var testApiClient = SetupClientAsInDependencyInjection(_messageHandler);

        await testApiClient.UpdateStock(productId: Guid.NewGuid(), unitsChange: 2);

        _messageHandler.NumberOfRequests.Should().Be(3);
        AssertRetryTimeBetween(_messageHandler.RequestTimes, requestIndex: 2, lowerLimit: 800, upperLimit: 1200);
        AssertRetryTimeBetween(_messageHandler.RequestTimes, requestIndex: 3, lowerLimit: 1800, upperLimit: 2200);
    }

    [Fact]
    public async Task WHEN_exceptions_received_and_then_ok_THEN_regular_policy_applies_until_ok()
    {
        _messageHandler = new HttpMessageHandlerWithRetries(null, null, HttpStatusCode.OK);
        var testApiClient = SetupClientAsInDependencyInjection(_messageHandler);

        await testApiClient.UpdateStock(productId: Guid.NewGuid(), unitsChange: 2);

        _messageHandler.NumberOfRequests.Should().Be(3);
        AssertRetryTimeBetween(_messageHandler.RequestTimes, requestIndex: 2, lowerLimit: 800, upperLimit: 1200);
        AssertRetryTimeBetween(_messageHandler.RequestTimes, requestIndex: 3, lowerLimit: 1800, upperLimit: 2200);
    }

    [Fact]
    public async Task WHEN_non_transient_response_is_received_twice_THEN_no_retries_are_made_at_all()
    {
        _messageHandler = new HttpMessageHandlerWithRetries(
            HttpStatusCode.TooManyRequests,
            HttpStatusCode.TooManyRequests);
        var testApiClient = SetupClientAsInDependencyInjection(_messageHandler);

        await testApiClient.UpdateStock(productId: Guid.NewGuid(), unitsChange: 2);

        _messageHandler.NumberOfRequests.Should().Be(1);
    }

    [Fact]
    public async Task WHEN_non_transient_and_then_transient_errors_THEN_no_retries_are_made()
    {
        _messageHandler = new HttpMessageHandlerWithRetries(
            HttpStatusCode.TooManyRequests,
            HttpStatusCode.InternalServerError,
            HttpStatusCode.InternalServerError,
            HttpStatusCode.OK);
        var testApiClient = SetupClientAsInDependencyInjection(_messageHandler);

        await testApiClient.UpdateStock(productId: Guid.NewGuid(), unitsChange: 2);

        _messageHandler.NumberOfRequests.Should().Be(1);
    }

    [Fact]
    public async Task WHEN_transient_error_then_not_transient_THEN_only_one_retry()
    {
        _messageHandler = new HttpMessageHandlerWithRetries(
            HttpStatusCode.InternalServerError,
            HttpStatusCode.TooManyRequests,
            HttpStatusCode.InternalServerError,
            HttpStatusCode.OK);
        var testApiClient = SetupClientAsInDependencyInjection(_messageHandler);

        await testApiClient.UpdateStock(productId: Guid.NewGuid(), unitsChange: 2);

        _messageHandler.NumberOfRequests.Should().Be(2);
        AssertRetryTimeBetween(_messageHandler.RequestTimes, requestIndex: 2, lowerLimit: 800, upperLimit: 1200);
    }

    private static IStockClient SetupClientAsInDependencyInjection(HttpMessageHandler messageHandler)
    {
        var servicesCollection = new ServiceCollection();

        var appSettingsStub = new Dictionary<string, string>
        {
            { "AnyClient:HttpClientsResiliency:RetriesTimes", RetriesSeconds }
        };

        var configuration = new ConfigurationBuilder().AddInMemoryCollection(appSettingsStub!).Build();

        servicesCollection.AddHttpClient<IStockClient, StockClient>(
                              client =>
                              {
                                  client.BaseAddress = new Uri("https://test.testing.com/");
                              })
                          .ConfigurePrimaryHttpMessageHandler(() => messageHandler)
                          .AddRetryPolicyForNotTooBigRequests(
                              configuration.GetSection("AnyClient:HttpClientsResiliency"));

        using var serviceProvider = servicesCollection.BuildServiceProvider();
        return serviceProvider.GetRequiredService<IStockClient>();
    }

    private static void AssertRetryTimeBetween(
        IReadOnlyDictionary<int, TimeSpan> messageHandlerRequestTimes,
        int requestIndex,
        int lowerLimit,
        int upperLimit)
    {
        var retryTime = messageHandlerRequestTimes[requestIndex] - messageHandlerRequestTimes[requestIndex - 1];
        retryTime.TotalMilliseconds.Should().BeGreaterThan(lowerLimit).And.BeLessThan(upperLimit);
    }
}