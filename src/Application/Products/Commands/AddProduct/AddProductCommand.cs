using CleanCompanyName.DDDMicroservice.Application.Products.Dto;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;

public class AddProductCommand : IRequest<ProductDto>
{
    public Guid Id { get; set; }
    public string Sku { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public Guid UserId { get; set; }
}