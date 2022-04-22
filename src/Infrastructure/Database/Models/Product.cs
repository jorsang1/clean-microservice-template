namespace Infrastructure.Database.Models;

public record Product(Guid Id,
                      string Sku,
                      string Title,
                      string Description,
                      decimal Price,
                      DateTime CreationDate,
                      string? CreationBy,
                      DateTime LastUpdate,
                      string? LastUpdateBy);
