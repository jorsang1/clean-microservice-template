using System.Text.Json;
using Application.Common.Interfaces;
using Infrastructure.Clients.StockClient.Configuration;
using Infrastructure.Clients.StockClient.Models;

namespace Infrastructure.Clients.StockClient;


public class StockClient : IStockClient
{
    private readonly static HttpClient HttpClient = new();

    public StockClient(StockClientConfiguration configuration)
    {
        HttpClient.BaseAddress = new Uri(configuration.BaseUrl);
        HttpClient.DefaultRequestHeaders.Add("Authorization", "PrivateKey " + configuration.Secret);
    }

    public async Task UpdateStock(Guid productId, int unitsChange)
    {
        var url = HttpClient.BaseAddress + "/stock-update/";
        var request = new UpdateStockRequest(productId, unitsChange);
        var requestContent = new StringContent(JsonSerializer.Serialize(request));
        await HttpClient.PostAsync(url, requestContent);
    }

}
