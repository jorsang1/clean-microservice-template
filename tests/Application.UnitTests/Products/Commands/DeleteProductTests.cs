using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.DeleteProduct;
using CleanCompanyName.DDDMicroservice.Application.UnitTests.Products.ExposedHandlers;
using FluentAssertions;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Application.UnitTests.Products.Commands;

public class DeleteProductTests : ProductTestBase
{
    [Fact]
    public async Task WHEN_not_providing_valid_Id_THEN_throws_not_found_exception()
    {
        var command = new DeleteProductCommand { Id = Guid.Empty };

        MockSetup
            .SetupRepositoryGetByIdValidResponse
            (
                ProductRepository,
                ProductBuilder.GetProductEmpty()
            );

        var requestHandler = new DeleteProductCommandHandlerExposed
        (
            ProductRepository.Object
        );

        await FluentActions
            .Invoking(() =>
                requestHandler.ExposedHandle(command, new CancellationToken()))
            .Should()
            .ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task WHEN_providing_valid_id_THEN_product_is_deleted()
    {
        var command = new DeleteProductCommand { Id = Guid.Empty };

        MockSetup
            .SetupRepositoryGetByIdValidResponse
            (
                ProductRepository,
                ProductBuilder.GetProduct()
            );

        var requestHandler = new DeleteProductCommandHandlerExposed
        (
            ProductRepository.Object
        );


        await FluentActions
            .Invoking(() =>
                requestHandler.ExposedHandle(command, new CancellationToken()))
            .Should()
            .NotThrowAsync<KeyNotFoundException>();
    }
}