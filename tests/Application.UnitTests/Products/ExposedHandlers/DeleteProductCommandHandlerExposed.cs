using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.DeleteProduct;
using Microsoft.Extensions.Logging;

namespace CleanCompanyName.DDDMicroservice.Application.UnitTests.Products.ExposedHandlers;

internal class DeleteProductCommandHandlerExposed : DeleteProductCommandHandler
{
    public DeleteProductCommandHandlerExposed(
        IProductRepository productRepository,
        ILogger<DeleteProductCommandHandlerExposed> logger)
        : base(
            productRepository,
            logger)
    {
    }

    public async Task ExposedHandle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await base.Handle(request, cancellationToken);
    }
}