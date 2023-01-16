using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetAllProducts;
using FluentAssertions;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Application.IntegrationTests.Products.Queries;

public class GetAllProductsTests : TestBase
{
    public GetAllProductsTests(Testing testing)
        : base(testing)
    {
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task WHEN_existing_products_on_repository_THEN_response_not_is_empty(int productsToAdd)
    {
        var currentProducts = await CountAsync();
        var product = ProductBuilder.GetProduct();

        for (var i = 0; i < productsToAdd; i++)
            await AddAsync(product);

        var result = await SendAsync(new GetAllProductsQuery());

        result.Should().NotBeEmpty();
        result.Should().HaveCount(currentProducts + productsToAdd);
    }
}