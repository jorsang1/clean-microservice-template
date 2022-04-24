using MediatR;
using Application.Common.Interfaces;
using Application.Products.DTOs;
using Application.Common.Exceptions;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Application.Products.Commands.AddProduct;

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
        AddAuditableInformation(productToAdd);

        if (productToAdd.IsValid())
        {
            var result = await _productRepository.Create(productToAdd);

            try
            {
                await _stockClient.UpdateStock(result.Id, 1);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating the stock for the product {Id}", result.Id);
                result.AddError(
                    "somecode",
                    "Product added but stock hasn't been updated.",
                    "Please update the stock later manually using the stocks endpoint. More info on: link to documentation"
                    );
            }

            return result.MapToDto();
        }
        else
        {
            throw new ValidationException(productToAdd.Errors.Adapt<List<ValidationError>>());
        }
    }

    private void AddAuditableInformation(Domain.Entities.Product.Product productToAdd)
    {
        productToAdd.Created = _dateService.Now;
        productToAdd.CreatedBy = Guid.NewGuid().ToString(); //TODO: Add identity service.
        productToAdd.LastModified = productToAdd.Created;
        productToAdd.LastModifiedBy = productToAdd.CreatedBy;
    }
}