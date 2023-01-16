using CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetAllProducts;

namespace CleanCompanyName.DDDMicroservice.Application.UnitTests.Products.Queries;

public class GetAllProductsTests : ProductTestBase
{
    private readonly GetAllProductsQueryHandler _sut;
    public GetAllProductsTests()
    {
        _sut = new GetAllProductsQueryHandler
        (
            ProductRepository.Object
        );
    }

    [Fact]
    public async Task WHEN_not_existing_products_on_repository_THEN_response_is_empty()
    {
        MockSetup.SetupRepositoryGetAllEmptyResponse(ProductRepository);

        var result = await _sut.Handle(new GetAllProductsQuery(), CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task WHEN_existing_products_on_repository_THEN_response_not_is_empty()
    {
        MockSetup.SetupRepositoryGetAllValidResponse(ProductRepository);

        var result = await _sut.Handle(new GetAllProductsQuery(), CancellationToken.None);

        result.Should().NotBeEmpty();
    }
}