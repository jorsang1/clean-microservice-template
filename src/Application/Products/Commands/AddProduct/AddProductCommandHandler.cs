using Microsoft.Extensions.Logging;
using Mapster;
using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Application.Products.Dto;
using CleanCompanyName.DDDMicroservice.Application.Common.Exceptions;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ProductDto>
{
    private IProductRepository _productRepository;
    private IDateTime _dateService;
    private IStockClient _stockClient;
    private ILogger<AddProductCommandHandler> _logger;

    public AddProductCommandHandler(IProductRepository productRepository, IDateTime dateService, IStockClient stockClient, ILogger<AddProductCommandHandler> logger)
    {
        _productRepository = productRepository;
        _dateService = dateService;
        _stockClient = stockClient;
        _logger = logger;
    }

    public async Task<ProductDto> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var productToAdd = request.MapToEntity();
        var validationResult = productToAdd.Validate();

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.Adapt<List<ValidationError>>());
        }

        AddAuditableInformation(productToAdd);
        var product = await _productRepository.Create(productToAdd);

        try
        {
            await _stockClient.UpdateStock(product.Id, 1);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating the stock for the product {Id}", product.Id);
        }

        return product.MapToDto();
    }

    private void AddAuditableInformation(Domain.Entities.Product.Product productToAdd)
    {
        productToAdd.CreatedOn = _dateService.Now;
        productToAdd.CreatedBy = Guid.NewGuid(); //TODO: Add identity service.
        productToAdd.LastModifiedOn = productToAdd.CreatedOn;
        productToAdd.LastModifiedBy = productToAdd.CreatedBy;
    }
}