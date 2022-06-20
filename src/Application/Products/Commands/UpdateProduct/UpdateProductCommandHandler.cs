using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Application.Products.Dto;
using CleanCompanyName.DDDMicroservice.Domain.Common.Exceptions;
using CleanCompanyName.DDDMicroservice.Domain.Common.Validators;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.UpdateProduct;

internal class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto?>
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
        var product = await _productRepository.GetById(request.Id);

        if (product is null || product.Id.Value == Guid.Empty)
            return null;

        var productToUpdate = request
            .BuildAdapter()
            .AddParameters("user", product.CreatedBy)
            .AdaptToType<Product>();

        var validationResult = await _validator.ValidateAsync(productToUpdate, cancellationToken);

        if (!validationResult.IsValid)
            throw new DomainValidationException(validationResult.Errors.MapToValidationErrors());

        product.ModifiedBy(Guid.NewGuid()); //TODO: Add identity service.

        try
        {
            await _productRepository.Update(productToUpdate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Problem updating the stock of the product on the stock service for the product {Id}, {Title}", request.Id, request.Title);
        }

        return productToUpdate.Adapt<ProductDto>();
    }
}