﻿using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.DeleteProduct;
using FluentAssertions;
using Mapster;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Application.IntegrationTests.Products.Commands;

public class DeleteProductTests : TestBase
{
    public DeleteProductTests(Testing testing)
        : base(testing)
    {
    }

    [Fact]
    public async Task WHEN_not_providing_valid_Id_THEN_throws_not_found_exception()
    {
        var command = new DeleteProductCommand { Id = Guid.Empty };

        await FluentActions.Invoking(() => SendAsync(command))
            .Should()
            .ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task WHEN_providing_valid_id_THEN_product_is_deleted()
    {
        var addProductCommand = ProductBuilder.GetProduct().Adapt<AddProductCommand>();
        var productCreated = await SendAsync(addProductCommand);

        var deleteProductCommand = new DeleteProductCommand { Id = productCreated.Id };
        await SendAsync(deleteProductCommand);

        var result = await GetById(productCreated.Id);

        result.Should().BeNull();
    }
}