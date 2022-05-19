using Microsoft.Extensions.Logging;
using Mapster;
using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Application.Products.Dto;
using CleanCompanyName.DDDMicroservice.Application.Common.Exceptions;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.UpdateProduct;

public class DeleteProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto?>
{
    private readonly IProductRepository _productRepository;
    private readonly IDateTime _dateService;
    private readonly ILogger _logger;

    public DeleteProductCommandHandler(IProductRepository productRepository, IDateTime dateService, ILogger<DeleteProductCommandHandler> logger)
    {
        _productRepository = productRepository;
        _dateService = dateService;
        _logger = logger;
    }

    public async Task<ProductDto?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productToUpdate = request.MapToEntity();
        var validationResult = productToUpdate.Validate();

        if (validationResult.IsValid!)
        {
            throw new ValidationException(validationResult.Errors.Adapt<List<ValidationError>>());
        }

        var product = await _productRepository.GetById(productToUpdate.Id);

        if (product is null)
            return null;

        productToUpdate.CreatedOn = product.CreatedOn;
        productToUpdate.CreatedBy = product.CreatedBy;
        UpdateAuditableInformation(productToUpdate);

        try
        {
            await _productRepository.Update(productToUpdate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Problem updating the stock of the product on the stock service for the product {Id}, {Title}.", request.Id, request.Title);
        }

        return productToUpdate.MapToDto();
        //Shall we maybe query the DB to make sure the operation is done??
        //return Task.FromResult(_productRepository.GetById(productToUpdate.Id).MapToDto());
    }

    private void UpdateAuditableInformation(Domain.Entities.Product.Product productToAdd)
    {
        productToAdd.LastModifiedOn = _dateService.Now;
        productToAdd.LastModifiedBy = Guid.Empty;
    }
}