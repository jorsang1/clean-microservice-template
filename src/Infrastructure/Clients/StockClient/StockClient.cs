using System.Text.Json;
using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Infrastructure.Clients.StockClient.Configuration;
using CleanCompanyName.DDDMicroservice.Infrastructure.Clients.StockClient.Models;

namespace CleanCompanyName.DDDMicroservice.Infrastructure.Clients.StockClient;


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