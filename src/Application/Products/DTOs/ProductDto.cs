namespace CleanCompanyName.DDDMicroservice.Application.Products.DTOs;

public record ProductDto(Guid Id, string Sku, string Title, string Description, decimal Price);