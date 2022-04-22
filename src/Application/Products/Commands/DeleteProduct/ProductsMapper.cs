using Application.Products.DTOs;

namespace Application.Products.Commands.DeleteProduct;
internal static class ProductsMapper
{
    public static ProductDto MapToDto(this Domain.Entities.Product.Product product)
    {
        return new ProductDto(product.Id, product.Sku, product.Title?.Value, product.Description ?? string.Empty, product.Price);
    }
}