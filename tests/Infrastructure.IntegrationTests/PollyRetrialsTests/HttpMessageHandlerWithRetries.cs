using System.Net;

namespace CleanCompanyName.DDDMicroservice.Infrastructure.IntegrationTests.PollyRetrialsTests;

public class HttpMessageHandlerWithRetries : HttpMessageHandler
{
    private readonly List<HttpResponseMessage?> _responseMessages;

    public HttpMessageHandlerWithRetries(params HttpStatusCode?[] statusCodes)
    {
        var responseMessages = statusCodes.Select(
                                              statusCode => statusCode.HasValue
                                                  ? BuildHttpResponseMessage(statusCode.Value)
                                                  : null)
                                          .ToList();
        _responseMessages = responseMessages;
    }

    public HttpMessageHandlerWithRetries(List<HttpResponseMessage?> responseMessages)
        => _responseMessages = responseMessages;

    public int NumberOfRequests { get; private set; }
    public Dictionary<int, TimeSpan> RequestTimes { get; } = new();

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        NumberOfRequests++;
        RequestTimes[NumberOfRequests] = TimeSpan.FromTicks(DateTime.UtcNow.Ticks);

        if (_responseMessages[NumberOfRequests - 1] is null)
            throw new HttpRequestException("Test exception");

        // We're doing this to consume the stream so we can test retries are still working after that.
        if (request.Content is not null)
            _ = await request.Content.ReadAsStringAsync(cancellationToken);

        var response = _responseMessages[NumberOfRequests - 1]!;
        response.RequestMessage = request;
        return await Task.FromResult(response);
    }

    private static HttpResponseMessage BuildHttpResponseMessage(HttpStatusCode statusCode)
    {
        var response = new HttpResponseMessage(statusCode);

        if (statusCode == HttpStatusCode.OK)
            response.Content = new StringContent(Guid.NewGuid().ToString());

        return response;
    }
}