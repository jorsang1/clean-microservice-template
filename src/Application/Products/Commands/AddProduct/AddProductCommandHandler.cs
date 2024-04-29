using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Application.Common.Mappers;
using CleanCompanyName.DDDMicroservice.Application.Products.Dto;
using CleanCompanyName.DDDMicroservice.Domain.Common.Validators;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using FluentResults;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;

internal class AddProductCommandHandler(
    IProductRepository productRepository,
    IDateTimeService dateTimeService,
    IStockClient stockClient,
    ILogger<AddProductCommandHandler> logger,
    IValidator<Product> validator)
    : IRequestHandler<AddProductCommand, Result<ProductDto>>
{
    private readonly ILogger _logger = logger;

    public async Task<Result<ProductDto>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var productToAdd = Product.Create(
            id: request.Id,
            sku: request.Sku,
            title: request.Title,
            description: request.Description,
            price: request.Price,
            createdOn: dateTimeService.Now,
            createdBy: request.UserId);

        var validationResult = await validator.ValidateAsync(productToAdd, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogInformation("Product requested to add is invalid. Errors: {Errors}", validationResult.Errors);
            var errors = validationResult.Errors.MapToValidationErrors().MapToFluentErrors();
            return Result.Fail(errors);
        }

        var product = await productRepository.Create(productToAdd);

        try
        {
            await stockClient.UpdateStock(product.Id.Value, 1);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating the stock for the product {Id}", product.Id.Value);
        }

        return product.Adapt<ProductDto>();
    }
}