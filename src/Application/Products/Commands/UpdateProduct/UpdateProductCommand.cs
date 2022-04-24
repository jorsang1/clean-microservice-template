using Application.Products.DTOs;
using MediatR;

namespace Application.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<ProductDto>
{
    public Guid Id { get; set; }
    public string Sku { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }

    public Guid UserId { get; set; }
};
