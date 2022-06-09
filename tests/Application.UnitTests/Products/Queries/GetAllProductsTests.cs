using System.Threading;
using System.Threading.Tasks;
using CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetAllProducts;
using FluentAssertions;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Application.UnitTests.Products.Queries;

public class GetAllProductsTests : ProductTestBase
{
    [Fact]
    public async Task WHEN_not_existing_products_on_repository_THEN_response_is_empty()
    {
        MockSetup.SetupRepositoryGetAllEmptyResponse(ProductRepository);

        var requestHandler = new GetAllProductsQueryHandler(ProductRepository.Object);

        var result = await requestHandler.Handle(new GetAllProductsQuery(), CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task WHEN_existing_products_on_repository_THEN_response_not_is_empty()
    {
        MockSetup.SetupRepositoryGetAllValidResponse(ProductRepository);

        var requestHandler = new GetAllProductsQueryHandler(ProductRepository.Object);

        var result = await requestHandler.Handle(new GetAllProductsQuery(), CancellationToken.None);

        result.Should().NotBeEmpty();
    }
}