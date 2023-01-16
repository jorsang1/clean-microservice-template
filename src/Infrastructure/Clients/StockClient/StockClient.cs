using System.Text.Json;
using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Infrastructure.Clients.StockClient.Configuration;
using CleanCompanyName.DDDMicroservice.Infrastructure.Clients.StockClient.Models;
using Microsoft.Extensions.Options;

namespace CleanCompanyName.DDDMicroservice.Infrastructure.Clients.StockClient;

internal class StockClient : IStockClient
{
    private readonly HttpClient _httpClient;

    public StockClient(HttpClient httpClient, IOptions<StockClientConfiguration> configuration)
    {
        _httpClient = httpClient;

        if (string.IsNullOrEmpty(configuration.Value.BaseUrl))
            throw new ArgumentNullException(nameof(configuration.Value.BaseUrl), "Base url for the stock client not provided in the configuration.");

        _httpClient.BaseAddress = new(configuration.Value.BaseUrl);
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"PrivateKey {configuration.Value.Secret}");
    }

    public async Task UpdateStock(Guid productId, int unitsChange)
    {
        var url = $"{_httpClient.BaseAddress}/stock-update/";
        var request = new UpdateStockRequest(productId, unitsChange);
        var requestContent = new StringContent(JsonSerializer.Serialize(request));
        await _httpClient.PostAsync(url, requestContent);
    }
}