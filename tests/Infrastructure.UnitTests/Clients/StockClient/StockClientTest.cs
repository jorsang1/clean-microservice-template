using System.Net;
using CleanCompanyName.DDDMicroservice.Infrastructure.Clients.StockClient.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace CleanCompanyName.DDDMicroservice.Infrastructure.UnitTests.Clients.StockClient;

/// <summary>
/// Right now this is only implementing some silly tests as the client is not really doing that much.
/// This is just an example and a placeholder for your client tests as following the 'honeycomb testing' we thing it could be important to unit test your clients as they are not going to be tested in your integration tests.
/// </summary>
public class StockClientTest
{
    [Fact]
    public async Task WHEN_called_api_works_THEN_no_throw()
    {
        var configuration = GetMockedConfiguration();
        var httpClient = GetHttpClientWithMockedMessageHandler();

        var sut = new Infrastructure.Clients.StockClient.StockClient(httpClient, configuration);

        await FluentActions
            .Awaiting(() =>
                sut.UpdateStock(Guid.Empty, 1))
            .Should()
            .NotThrowAsync();
    }

    [Fact]
    public async Task WHEN_called_api_has_problems_THEN_throws()
    {
        var configuration = GetMockedConfiguration();
        var httpClient = GetThrowingHttpClientWithMockedMessageHandler();

        var sut = new Infrastructure.Clients.StockClient.StockClient(httpClient, configuration);

        await FluentActions
            .Awaiting(() =>
                sut.UpdateStock(Guid.Empty, 1))
            .Should()
            .ThrowAsync<Exception>();
    }

    private IOptions<StockClientConfiguration> GetMockedConfiguration()
    {
        var stockClientConfiguration = new StockClientConfiguration() { BaseUrl = "http://dummy.test", Secret = "dummy" };
        var mockConfiguration = new Mock<IOptions<StockClientConfiguration>>();
        mockConfiguration.Setup(config => config.Value).Returns(stockClientConfiguration);
        return mockConfiguration.Object;
    }

    private HttpClient GetHttpClientWithMockedMessageHandler()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(""),
            })
            .Verifiable();
        return new HttpClient(handlerMock.Object);
    }

    private HttpClient GetThrowingHttpClientWithMockedMessageHandler()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ThrowsAsync(new Exception())
            .Verifiable();
        return new HttpClient(handlerMock.Object);
    }
}