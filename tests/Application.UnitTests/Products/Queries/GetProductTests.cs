using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetProduct;

namespace CleanCompanyName.DDDMicroservice.Application.UnitTests.Products.Queries;

public class GetProductTests : ProductTestBase
{
    private readonly GetProductQueryHandler _sut;
    public GetProductTests()
    {
        _sut = new GetProductQueryHandler
        (
            ProductRepository.Object
        );
    }

    [Fact]
    public async Task WHEN_not_providing_valid_Id_THEN_product_is_not_found()
    {
        var product = ProductBuilder.GetProductEmpty();
        var request = new GetProductQuery(Guid.Empty);
        MockSetup.SetupRepositoryGetByIdValidResponse(ProductRepository, product);

        var result = await _sut.Handle(request, CancellationToken.None);

        result.Should().BeNull();
    }

    [Fact]
    public async Task WHEN_providing_valid_id_THEN_product_is_found()
    {
        var product = ProductBuilder.GetProduct();
        var request = new GetProductQuery(product.Id.Value);
        MockSetup.SetupRepositoryGetByIdValidResponse(ProductRepository, product);

        var result = await _sut.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
    }
}