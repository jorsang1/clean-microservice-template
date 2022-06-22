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
        var request = new GetProductQuery { ProductId = product.Id.Value };
        await AddAsync(product);

        var result = await SendAsync(request);

        result.Should().NotBeNull();
        result!.Id.Should().Be(request.ProductId);
    }
}