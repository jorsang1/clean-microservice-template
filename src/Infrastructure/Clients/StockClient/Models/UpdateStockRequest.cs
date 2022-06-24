namespace CleanCompanyName.DDDMicroservice.Infrastructure.Clients.StockClient.Models;

internal sealed record UpdateStockRequest(Guid ProductId, int UnitsChange);