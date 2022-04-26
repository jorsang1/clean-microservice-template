using CleanCompanyName.DDDMicroservice.Application.Products.DTOs;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.DeleteProduct;

internal static class ProductsMapper
{
    public static ProductDto MapToDto(this Domain.Entities.Product.Product product)
    {
        return new ProductDto(product.Id, product.Sku, product.Title?.Value, product.Description ?? string.Empty, product.Price);
    }
}