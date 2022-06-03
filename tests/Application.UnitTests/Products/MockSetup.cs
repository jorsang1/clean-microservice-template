using System;
using System.Collections.Generic;
using System.Threading;
using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Domain.Common.Exceptions;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace CleanCompanyName.DDDMicroservice.Application.UnitTests.Products;

public class MockSetup
{
    internal void SetupValidationErrorResponse(Mock<IValidator<Product>> validator)
    {
        validator
            .Setup
            (
                mockValidator => mockValidator.ValidateAsync(
                    It.IsAny<Product>(),
                    It.IsAny<CancellationToken>())
            ).ReturnsAsync
            (
                new ValidationResult { Errors = { new ValidationFailure("Error", "error", 0) }}
            );
    }

    internal void SetupValidationValidResponse(Mock<IValidator<Product>> validator)
    {
        validator
            .Setup
            (
                mockValidator => mockValidator.ValidateAsync(
                    It.IsAny<Product>(),
                    It.IsAny<CancellationToken>())
            ).ReturnsAsync
            (
                new ValidationResult()
            );
    }

    internal void SetupRepositoryCreateValidResponse(Mock<IProductRepository> productRepository, Product product)
    {
        productRepository
            .Setup(repo => repo.Create(It.IsAny<Product>()))
            .ReturnsAsync(product);
    }

    internal void SetupRepositoryGetByIdValidResponse(Mock<IProductRepository> productRepository, Product product)
    {
        productRepository
            .Setup(repo => repo.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(product);
    }

    internal void SetupRepositoryGetAllEmptyResponse(Mock<IProductRepository> productRepository)
    {
        productRepository
            .Setup(repo => repo.GetAll())
            .ReturnsAsync(new List<Product>());
    }

    internal void SetupRepositoryGetAllValidResponse(Mock<IProductRepository> productRepository)
    {
        productRepository
            .Setup(repo => repo.GetAll())
            .ReturnsAsync(new List<Product> { ProductBuilder.GetProduct() });
    }
}