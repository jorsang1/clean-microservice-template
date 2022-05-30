namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.DeleteProduct;

public record struct DeleteProductCommand : IRequest
{
    public Guid Id { get; set; }
}