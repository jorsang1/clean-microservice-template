using System.Threading.Tasks;
using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;
using CleanCompanyName.DDDMicroservice.Domain.Common.Exceptions;
using FluentAssertions;
using Mapster;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Application.IntegrationTests.Products.Commands;

using static Testing;

public class AddProductTests : IClassFixture<Testing>
{
    [Fact]
    public async Task WHEN_no_fields_are_filled_THEN_throws_validation_exception()
    {
        await FluentActions.Invoking(() =>
            SendAsync(ProductBuilder.GetProductEmpty().Adapt<AddProductCommand>()))
            .Should()
            .ThrowAsync<DomainValidationException>();
    }

    [Fact]
    public async Task WHEN_few_fields_are_filled_THEN_throws_validation_exception()
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

        var result = await GetById(productCreated.Id);

        result.Should().NotBeNull();
        result!.Sku.Should().Be(command.Sku);
        result.Title.Title.Should().Be(command.Title);
    }
}