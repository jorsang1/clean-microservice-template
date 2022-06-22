using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;

namespace CleanCompanyName.DDDMicroservice.Application.UnitTests.Products;

public abstract class ProductTestBase : IClassFixture<MapperConfigSetup>
{
    protected readonly MockSetup MockSetup = new();
    protected readonly Mock<IProductRepository> ProductRepository = new();
}