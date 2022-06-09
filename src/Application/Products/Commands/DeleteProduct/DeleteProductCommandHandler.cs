using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.DeleteProduct;

internal class DeleteProductCommandHandler : AsyncRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger _logger;

    public DeleteProductCommandHandler(
        IProductRepository productRepository,
        ILogger<DeleteProductCommandHandler> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    protected override async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(request.Id);

        if (product is null || product.Id == Guid.Empty)
        {
            //TODO: Switch to a EntityDoesntExistException or similar.
            throw new KeyNotFoundException("The product to delete doesn't exists.");
        }

        try
        {
            await _productRepository.Delete(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Problem deleting the product {Id}", request.Id);
        }
    }
}