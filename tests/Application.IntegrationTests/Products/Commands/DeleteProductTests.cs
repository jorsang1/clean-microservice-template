using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.DeleteProduct;
using FluentAssertions;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Application.IntegrationTests.Products.Commands;

public class DeleteProductTests : TestBase
{
    public DeleteProductTests(Testing testing)
        : base(testing)
    {
    }

    [Fact]
    public async Task WHEN_not_providing_valid_Id_THEN_Returns_error_result()
    {
        var command = new DeleteProductCommand(Guid.Empty);

        var productDeleteResult = await SendAsync(command);

        productDeleteResult.IsSuccess.Should().BeFalse();
        productDeleteResult.Errors.First().Message.Should().Be("Id not found.");
        //TODO: Add more tests or asserts
    }

    [Fact]
    public async Task WHEN_providing_valid_id_THEN_product_is_deleted()
    {
        var product = ProductBuilder.GetProduct();
        await AddAsync(product);

        var deleteProductCommand = new DeleteProductCommand(product.Id.Value);
        await SendAsync(deleteProductCommand);

        var result = await GetById(product.Id.Value);

        result.Should().BeNull();
    }
}