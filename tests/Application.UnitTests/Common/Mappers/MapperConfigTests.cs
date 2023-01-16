using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Application.Products.Dto;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using Mapster;

namespace CleanCompanyName.DDDMicroservice.Application.UnitTests.Common.Mappers;

public class MapperConfigTests: IClassFixture<MapperConfigSetup>
{
    private const string TestTitle = "some title";
    private static readonly Guid TestGuid = Guid.NewGuid();


    [Fact]
    public void WHEN_Title_is_specified_on_Product_THEN_ProductDto_should_have_Title_specified()
    {
        var source = ProductBuilder.GetProductWithTitle(TestTitle);
        var result = source.Adapt<ProductDto>();
        result.Title.Should().Be(TestTitle);
    }

    [Fact]
    public void WHEN_Title_is_specified_on_AddProductCommand_THEN_Product_should_have_Title_specified()
    {
        var source = AddProductCommandBuilder.GetAddProductCommandWithTitle(TestTitle);
        var result = source.Adapt<Product>();
        result.Title.Value.Should().Be(TestTitle);
    }

    [Fact]
    public void WHEN_Title_is_specified_on_UpdateProductCommand_THEN_Product_should_have_Title_specified()
    {
        var source = UpdateProductCommandBuilder.GetUpdateProductCommandWithTitle(TestTitle);
        var result = source.Adapt<Product>();
        result.Title.Value.Should().Be(TestTitle);
    }

    [Fact]
    public void WHEN_Id_is_specified_on_Product_THEN_ProductDto_should_have_Id_specified()
    {
        var source = ProductBuilder.GetProductWithId(TestGuid);
        var result = source.Adapt<ProductDto>();
        result.Id.Should().Be(TestGuid);
    }
}