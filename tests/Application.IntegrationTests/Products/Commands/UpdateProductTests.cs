using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.UpdateProduct;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products.ValueObjects;
using FluentAssertions;
using Mapster;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Application.IntegrationTests.Products.Commands;

public class UpdateProductTests : TestBase
{
    public UpdateProductTests(Testing testing)
        : base(testing)
    {
    }

    [Fact]
    public async Task WHEN_updating_product_that_does_not_exist_on_repository_THEN_does_not_return_product()
    {
        var product = ProductBuilder.GetProduct();
        var command = product.Adapt<UpdateProductCommand>();

        var productUpdatedResult = await SendAsync(command);

        productUpdatedResult.IsSuccess.Should().BeFalse();
        productUpdatedResult.Errors.First().Message.Should().Be("Id not found.");
        //TODO: Add more tests or asserts
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
        result.Title.Value.Should().Be(productUpdatedValue.Title);
    }

    [Fact]
    public async Task WHEN_few_fields_are_filled_THEN_returns_validation_error()
    {
        var product = ProductBuilder.GetProduct();

        await AddAsync(product);

        var updatedProduct = new Product(product.Id, string.Empty, new ProductTitle(string.Empty), product.Description, product.Price, product.CreatedOn,
            product.CreatedBy, product.LastModifiedOn, product.LastModifiedBy);
        var command = updatedProduct.Adapt<UpdateProductCommand>();
        var productUpdatedResult = await SendAsync(command);

        productUpdatedResult.Should().NotBeNull();
        productUpdatedResult!.IsSuccess.Should().BeFalse();
        productUpdatedResult!.Errors.Should().NotBeEmpty();
        productUpdatedResult!.Errors.First().Message.Should().Be("'Sku' must not be empty.");
        //TODO: Add more test cases or assertions
    }

}