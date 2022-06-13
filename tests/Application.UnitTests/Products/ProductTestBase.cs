using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using Moq;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Application.UnitTests.Products;

public abstract class ProductTestBase : IClassFixture<MapperConfigSetup>
{
    protected readonly MockSetup MockSetup;
    protected readonly Mock<IProductRepository> ProductRepository;

    protected ProductTestBase()
    {
        MockSetup = new MockSetup();
        ProductRepository = new Mock<IProductRepository>();
    }
}