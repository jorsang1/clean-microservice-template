using System.Text.Json;
using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Infrastructure.Clients.StockClient.Configuration;
using CleanCompanyName.DDDMicroservice.Infrastructure.Clients.StockClient.Models;

namespace CleanCompanyName.DDDMicroservice.Infrastructure.Clients.StockClient;


internal class StockClient : IStockClient
{
    private readonly static HttpClient HttpClient = new();

    public StockClient(StockClientConfiguration configuration)
    {
        if (string.IsNullOrEmpty(configuration.BaseUrl))
            throw new ArgumentNullException(nameof(configuration.BaseUrl), "Base url for the stock client not provided in the configuration.");

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