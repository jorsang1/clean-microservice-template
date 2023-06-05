using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;
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
    public async Task WHEN_no_fields_are_filled_THEN_result_should_contain_the_error()
    {
        var command = ProductBuilder.GetProductEmpty().Adapt<AddProductCommand>();

        var result = await SendAsync(command);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.First().Message.Should().Be("'Id Value' must not be empty.");
        //TODO: Add more tests or asserts
    }

    [Fact]
    public async Task WHEN_only_Sku_is_filled_THEN_result_should_contain_the_error()
    {
        var command = ProductBuilder.GetProductWithSku().Adapt<AddProductCommand>();

        var result = await SendAsync(command);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.First().Message.Should().Be("'Id Value' must not be empty.");
        //TODO: Add more tests or asserts
    }

    [Fact]
    public async Task WHEN_all_fields_are_filled_THEN_product_is_created()
    {
        var command = ProductBuilder.GetProduct().Adapt<AddProductCommand>();

        var result = await SendAsync(command);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Sku.Should().Be(command.Sku);
        result.Value.Title.Should().Be(command.Title);
    }
}