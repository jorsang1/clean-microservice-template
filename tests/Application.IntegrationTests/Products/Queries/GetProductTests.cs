using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetProduct;
using FluentAssertions;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Application.IntegrationTests.Products.Queries;

public class GetProductTests : TestBase
{
    public GetProductTests(Testing testing)
    : base(testing)
    {
    }

    [Fact]
    public async Task WHEN_providing_valid_id_THEN_product_is_found()
    {
        var product = ProductBuilder.GetProduct();
        var request = new GetProductQuery { ProductId = product.Id };
        await AddAsync(product);

        var result = await SendAsync(request);

        result.Should().NotBeNull();

        var productDto = result!.Value;

        productDto.Id.Should().Be(request.ProductId);
    }
}