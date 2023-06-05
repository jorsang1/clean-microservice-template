using FluentResults;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.DeleteProduct;

public readonly record struct DeleteProductCommand(Guid Id) : IRequest<Result>;