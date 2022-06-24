namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(Guid Id) : IRequest;