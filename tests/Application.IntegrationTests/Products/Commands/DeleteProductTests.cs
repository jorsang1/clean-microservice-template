using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.DeleteProduct;
using FluentAssertions;
using Mapster;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Application.IntegrationTests.Products.Commands;

using static Testing;

public class DeleteProductTests : IClassFixture<Testing>
{
    [Fact]
    public async Task WHEN_not_providing_valid_Id_THEN_throws_not_found_exception()
    {
        var command = new DeleteProductCommand { Id = Guid.Empty };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task WHEN_providing_valid_id_THEN_product_is_deleted()
    {
        var productCreated =
            await SendAsync
            (
                ProductBuilder.GetProduct().Adapt<AddProductCommand>()
            );

        await SendAsync
        (
            new DeleteProductCommand
            {
                Id = productCreated.Id
            }
        );

        var result = await GetById(productCreated.Id);
        
        result.Should().BeNull();
    }
}