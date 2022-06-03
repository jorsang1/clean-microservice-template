using System.Threading.Tasks;
using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.UpdateProduct;
using FluentAssertions;
using Mapster;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Application.IntegrationTests.Products.Commands;

using static Testing;

public class UpdateProductTests : IClassFixture<Testing>
{
    [Fact]
    public async Task WHEN_updating_product_that_does_not_exist_on_repository_THEN_does_not_return_product()
    {
        var product = ProductBuilder.GetProduct();
        var command = product.Adapt<UpdateProductCommand>();

        var productUpdated = await SendAsync(command);

        productUpdated.Should().BeNull();
    }


    [Fact]
    public async Task WHEN_all_fields_are_filled_THEN_product_is_updated()
    {
        var product = ProductBuilder.GetProduct();
        var command = product.Adapt<UpdateProductCommand>();

        await AddAsync(product);

        var productUpdated = await SendAsync(command);

        productUpdated.Should().NotBeNull();

        var productUpdatedValue = productUpdated!.Value;
        var result = await GetById(productUpdated!.Value.Id);

        result.Should().NotBeNull();
        result!.Sku.Should().Be(productUpdatedValue.Sku);
        result.Title.Title.Should().Be(productUpdatedValue.Title);
    }
}