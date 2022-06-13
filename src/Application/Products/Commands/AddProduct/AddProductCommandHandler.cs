﻿using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Application.Products.Dto;
using CleanCompanyName.DDDMicroservice.Domain.Common.Exceptions;
using CleanCompanyName.DDDMicroservice.Domain.Common.Validators;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;

internal class AddProductCommandHandler : IRequestHandler<AddProductCommand, ProductDto>
{
    private readonly IDateTime _dateService;
    private readonly ILogger _logger;
    private readonly IProductRepository _productRepository;
    private readonly IStockClient _stockClient;
    private readonly IValidator<Product> _validator;

    public AddProductCommandHandler(
        IProductRepository productRepository,
        IDateTime dateService,
        IStockClient stockClient,
        ILogger<AddProductCommandHandler> logger,
        IValidator<Product> validator)
    {
        _productRepository = productRepository;
        _dateService = dateService;
        _stockClient = stockClient;
        _logger = logger;
        _validator = validator;
    }

    public async Task<ProductDto> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var productToAdd = request.Adapt<Product>();

        var validationResult = await _validator.ValidateAsync(productToAdd, cancellationToken);

        if (!validationResult.IsValid)
            throw new DomainValidationException(validationResult.Errors.MapToValidationErrors());

        AddAuditableInformation(productToAdd);
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

    private void AddAuditableInformation(Product productToAdd)
    {
        productToAdd.CreatedOn = _dateService.Now;
        productToAdd.CreatedBy = Guid.NewGuid(); //TODO: Add identity service.
        productToAdd.LastModifiedOn = productToAdd.CreatedOn;
        productToAdd.LastModifiedBy = productToAdd.CreatedBy;
    }
}