namespace CleanCompanyName.DDDMicroservice.Infrastructure.Database.Models;

public record Product(
    Guid Id,
    string Sku,
    string Title,
    string Description,
    decimal Price,
    DateTime CreationDate,
    Guid? CreationBy,
    DateTime LastUpdate,
    Guid? LastUpdateBy);