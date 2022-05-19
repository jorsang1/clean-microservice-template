namespace CleanCompanyName.DDDMicroservice.Application.Products.Dto;

public record ProductDto(Guid Id, string Sku, string Title, string Description, decimal Price);