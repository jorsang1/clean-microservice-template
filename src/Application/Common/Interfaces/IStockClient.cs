namespace CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;

public interface IStockClient
{
    Task UpdateStock(Guid productId, int unitsChange);
}