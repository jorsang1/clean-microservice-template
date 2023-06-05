using CleanCompanyName.DDDMicroservice.Application.Common;
using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Application.Common.Mappers;
using CleanCompanyName.DDDMicroservice.Application.Products.Dto;
using CleanCompanyName.DDDMicroservice.Domain.Common.Validators;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using FluentResults;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.UpdateProduct;

internal class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger _logger;
    private readonly IValidator<Product> _validator;

    public UpdateProductCommandHandler(
        IProductRepository productRepository,
        IDateTimeService dateTimeService,
        ILogger<UpdateProductCommandHandler> logger,
        IValidator<Product> validator)
    {
        _productRepository = productRepository;
        _dateTimeService = dateTimeService;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productToUpdate = await _productRepository.GetById(request.Id);

        if (productToUpdate is null || productToUpdate.Id.Value == Guid.Empty)
            return Result.Fail(new Error("Id not found.") { Metadata = { {"Status", ResultStatus.NotFound} }});

        productToUpdate.Update(
            sku: request.Sku,
            title: request.Title,
            description: request.Description,
            price: request.Price,
            modifiedOn: _dateTimeService.Now,
            modifiedBy: request.UserId);

        var validationResult = await _validator.ValidateAsync(productToUpdate, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogInformation("Product requested to update is invalid. Errors: {Errors}", validationResult.Errors);
            var errors = validationResult.Errors.MapToValidationErrors().MapToFluentErrors();
            return Result.Fail(errors);
        }

        try
        {
            await _productRepository.Update(productToUpdate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Problem updating the stock of the product on the stock service for the product {Id}, {Title}", request.Id, request.Title);
        }

        return Result.Ok(productToUpdate.Adapt<ProductDto>());
    }
}