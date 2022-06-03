using System.Threading.Tasks;
using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetAllProducts;
using FluentAssertions;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Application.IntegrationTests.Products.Queries;

using static Testing;

public class GetAllProductsTests : IClassFixture<Testing>
{
    [Fact]
    public async Task WHEN_existing_products_on_repository_THEN_response_not_is_empty()
    {
        var product = ProductBuilder.GetProduct();
        await AddAsync(product);

        var result = await SendAsync(new GetAllProductsQuery());

        result.Should().NotBeEmpty();
        result.Should().HaveCountGreaterThanOrEqualTo(1);
    }
}