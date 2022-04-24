using MediatR;
using Application.Common.Interfaces;
using Application.Products.DTOs;
using Application.Common.Exceptions;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Application.Products.Commands.UpdateProduct;

public class DeleteProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto?>
{
    private IProductRepository _productRepository;
    private IDateTime _dateService;
    private ILogger _logger;
    public DeleteProductCommandHandler(IProductRepository productRepository, IDateTime dateService, ILogger logger)
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
        productToAdd.LastModifiedBy = Guid.Empty.ToString();
    }
}