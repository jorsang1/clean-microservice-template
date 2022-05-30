using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Application.Products.Dto;
using CleanCompanyName.DDDMicroservice.Domain.Common.Exceptions;
using CleanCompanyName.DDDMicroservice.Domain.Common.Validators;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Product;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto?>
{
    private readonly IProductRepository _productRepository;
    private readonly IDateTime _dateService;
    private readonly ILogger _logger;
    private readonly IValidator<Product> _validator;

    public UpdateProductCommandHandler(
        IProductRepository productRepository,
        IDateTime dateService,
        ILogger<UpdateProductCommandHandler> logger,
        IValidator<Product> validator)
    {
        _productRepository = productRepository;
        _dateService = dateService;
        _logger = logger;
        _validator = validator;
    }

    public async Task<ProductDto?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productToUpdate = request.MapToEntity();

        var validationResult = await _validator.ValidateAsync(productToUpdate, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new DomainValidationException(validationResult.Errors.MapToValidationErrors());
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

    private void UpdateAuditableInformation(Product productToAdd)
    {
        productToAdd.LastModifiedOn = _dateService.Now;
        productToAdd.LastModifiedBy = Guid.Empty;
    }
}