namespace CleanCompanyName.DDDMicroservice.Infrastructure.Clients.StockClient.Models;

internal readonly record struct UpdateStockRequest(Guid ProductId, int UnitsChange);