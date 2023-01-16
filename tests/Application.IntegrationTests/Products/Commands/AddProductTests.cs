using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;
using CleanCompanyName.DDDMicroservice.Domain.Common.Exceptions;
using FluentAssertions;
using Mapster;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Application.IntegrationTests.Products.Commands;

public class AddProductTests : TestBase
{
    public AddProductTests(Testing testing)
        : base(testing)
    {
    }

    [Fact]
    public async Task WHEN_no_fields_are_filled_THEN_throws_validation_exception()
    {
        await FluentActions.Invoking(() =>
            SendAsync(ProductBuilder.GetProductEmpty().Adapt<AddProductCommand>()))
            .Should()
            .ThrowAsync<DomainValidationException>();
    }

    [Fact]
    public async Task WHEN_only_Sku_is_filled_THEN_throws_validation_exception()
    {
        await FluentActions.Invoking(() =>
            SendAsync(ProductBuilder.GetProductWithSku().Adapt<AddProductCommand>()))
            .Should()
            .ThrowAsync<DomainValidationException>();
    }

    [Fact]
    public async Task WHEN_all_fields_are_filled_THEN_product_is_created()
    {
        var command = ProductBuilder.GetProduct().Adapt<AddProductCommand>();

        var productCreated = await SendAsync(command);

        //var result = await GetById(productCreated.Id);

        productCreated.Should().NotBeNull();
        productCreated!.Sku.Should().Be(command.Sku);
        productCreated.Title.Should().Be(command.Title);
    }
}