using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Application.Products.Dto;
using CleanCompanyName.DDDMicroservice.Domain.Common.Exceptions;
using CleanCompanyName.DDDMicroservice.Domain.Common.Validators;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;

internal class AddProductCommandHandler : IRequestHandler<AddProductCommand, ProductDto>
{
    private readonly ILogger _logger;
    private readonly IProductRepository _productRepository;
    private readonly IStockClient _stockClient;
    private readonly IValidator<Product> _validator;

    public AddProductCommandHandler(
        IProductRepository productRepository,
        IStockClient stockClient,
        ILogger<AddProductCommandHandler> logger,
        IValidator<Product> validator)
    {
        _productRepository = productRepository;
        _stockClient = stockClient;
        _logger = logger;
        _validator = validator;
    }

    public async Task<ProductDto> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var productToAdd = new Product(
            id: request.Id,
            sku: request.Sku,
            title: request.Title,
            description: request.Description,
            price: request.Price,
            createdBy: Guid.NewGuid()); // TODO replace with proper user identity

        var validationResult = await _validator.ValidateAsync(productToAdd, cancellationToken);

        if (!validationResult.IsValid)
            throw new DomainValidationException(validationResult.Errors.MapToValidationErrors());

        var product = await _productRepository.Create(productToAdd);

        try
        {
            await _stockClient.UpdateStock(product.Id.Value, 1);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating the stock for the product {Id}", product.Id.Value);
        }

        return product.Adapt<ProductDto>();
    }
}