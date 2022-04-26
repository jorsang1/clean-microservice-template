namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest
{
    public Guid Id { get; set; }
}