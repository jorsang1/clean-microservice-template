using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Application.Common.Mappers;
using CleanCompanyName.DDDMicroservice.Application.Products.Dto;
using CleanCompanyName.DDDMicroservice.Domain.Common.Validators;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using FluentResults;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;

internal class AddProductCommandHandler : IRequestHandler<AddProductCommand, Result<ProductDto>>
{
    private readonly ILogger _logger;
    private readonly IProductRepository _productRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly IStockClient _stockClient;
    private readonly IValidator<Product> _validator;

    public AddProductCommandHandler(
        IProductRepository productRepository,
        IDateTimeService dateTimeService,
        IStockClient stockClient,
        ILogger<AddProductCommandHandler> logger,
        IValidator<Product> validator)
    {
        _productRepository = productRepository;
        _dateTimeService = dateTimeService;
        _stockClient = stockClient;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<ProductDto>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var productToAdd = Product.Create(
            id: request.Id,
            sku: request.Sku,
            title: request.Title,
            description: request.Description,
            price: request.Price,
            createdOn: _dateTimeService.Now,
            createdBy: request.UserId);

        var validationResult = await _validator.ValidateAsync(productToAdd, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogInformation("Product requested to add is invalid. Errors: {Errors}", validationResult.Errors);
            var errors = validationResult.Errors.MapToValidationErrors().MapToFluentErrors();
            return Result.Fail(errors);
        }

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