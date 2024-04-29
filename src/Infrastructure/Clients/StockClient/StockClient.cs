using System.Text.Json;
using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Infrastructure.Clients.StockClient.Models;

namespace CleanCompanyName.DDDMicroservice.Infrastructure.Clients.StockClient;

internal class StockClient(HttpClient httpClient) : IStockClient
{
    public async Task UpdateStock(Guid productId, int unitsChange)
    {
        var url = $"{httpClient.BaseAddress}/stock-update/";
        var request = new UpdateStockRequest(productId, unitsChange);
        var requestContent = new StringContent(JsonSerializer.Serialize(request));
        await httpClient.PostAsync(url, requestContent);
    }
}