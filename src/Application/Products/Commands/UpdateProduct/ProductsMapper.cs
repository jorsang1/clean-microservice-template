using CleanCompanyName.DDDMicroservice.Application.Products.Dto;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.UpdateProduct;

internal static class ProductsMapper
{
    public static Domain.Entities.Product.Product MapToEntity(this UpdateProductCommand dto)
    {
        return new Domain.Entities.Product.Product
        {
            Id = dto.Id,
            Sku = dto.Sku,
            Title = new Domain.Entities.Product.ValueObjects.ProjectTitle(dto.Title),
            Description = dto.Description ?? string.Empty,
            Price = 0,
        };
    }

    public static ProductDto MapToDto(this Domain.Entities.Product.Product product)
    {
        return new ProductDto(product.Id, product.Sku, product.Title?.Value, product.Description ?? string.Empty, product.Price);
    }
}