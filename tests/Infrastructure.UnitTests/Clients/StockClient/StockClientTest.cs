using System.Net;
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
        var httpClient = GetHttpClientWithMockedMessageHandler();

        var sut = new Infrastructure.Clients.StockClient.StockClient(httpClient);

        await FluentActions
            .Awaiting(() =>
                sut.UpdateStock(Guid.Empty, 1))
            .Should()
            .NotThrowAsync();
    }

    [Fact]
    public async Task WHEN_called_api_has_problems_THEN_throws()
    {
        var httpClient = GetThrowingHttpClientWithMockedMessageHandler();

        var sut = new Infrastructure.Clients.StockClient.StockClient(httpClient);

        await FluentActions
            .Awaiting(() =>
                sut.UpdateStock(Guid.Empty, 1))
            .Should()
            .ThrowAsync<Exception>();
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
        return new HttpClient(handlerMock.Object) { BaseAddress = new Uri("https://somewhere.com/")};
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