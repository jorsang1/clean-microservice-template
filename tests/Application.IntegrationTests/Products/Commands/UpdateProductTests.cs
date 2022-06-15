using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.UpdateProduct;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using FluentAssertions;
using Mapster;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Application.IntegrationTests.Products.Commands;

public class UpdateProductTests : TestBase
{
    private readonly Product _product =
        ProductBuilder
            .Init()
            .WithAllData()
            .Get();

    public UpdateProductTests(Testing testing)
        : base(testing)
    {
    }

    [Fact]
    public async Task WHEN_updating_product_that_does_not_exist_on_repository_THEN_does_not_return_product()
    {
        var command = _product.Adapt<UpdateProductCommand>();

        var productUpdated = await SendAsync(command);

        productUpdated.Should().BeNull();
    }

    [Fact]
    public async Task WHEN_all_fields_are_filled_THEN_product_is_updated()
    {
        var command = _product.Adapt<UpdateProductCommand>();

        await AddAsync(_product);

        var productUpdated = await SendAsync(command);

        productUpdated.Should().NotBeNull();

        var productUpdatedValue = productUpdated!.Value;
        var result = await GetById(productUpdated!.Value.Id);

        result.Should().NotBeNull();
        result!.Sku.Should().Be(productUpdatedValue.Sku);
        result.Title.Value.Should().Be(productUpdatedValue.Title);
    }
}