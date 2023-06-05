using CleanCompanyName.DDDMicroservice.Application.Common;
using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.DeleteProduct;

internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
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

    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var productToDelete = await _productRepository.GetById(request.Id);

        if (productToDelete is null || productToDelete.Id.Value == Guid.Empty)
            return Result.Fail(new Error("Id not found.") { Metadata = { {"Status", ResultStatus.NotFound} }});

        await _productRepository.Delete(productToDelete);

        _logger.LogInformation("Product {ProductId} deleted.", request.Id);
        return Result.Ok();
    }
}