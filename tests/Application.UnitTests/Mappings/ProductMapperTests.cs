using CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.UpdateProduct;
using CleanCompanyName.DDDMicroservice.Application.Products.Dto;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products.ValueObjects;
using FluentAssertions;
using Mapster;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Application.UnitTests.Mappings;

public class ProductMapperTests : IClassFixture<DependencyInjectionFixture>
{
    private const string TestTitle = "some title";

    [Fact]
    public void WHEN_Title_is_specified_on_Product_THEN_ProductListItemDto_should_have_Title_specified()
    {
        var source = new Product
        {
            Title = new ProjectTitle(TestTitle)
        };

        var result = source.Adapt<ProductListItemDto>();
        result.Title.Should().Be(TestTitle);
    }

    [Fact]
    public void WHEN_Title_is_specified_on_Product_THEN_ProductDto_should_have_Title_specified()
    {
        var source = new Product
        {
            Title = new ProjectTitle(TestTitle)
        };

        var result = source.Adapt<ProductDto>();
        result.Title.Should().Be(TestTitle);
    }

    [Fact]
    public void WHEN_Title_is_specified_on_AddProductCommand_THEN_Product_should_have_Title_specified()
    {
        var source = new AddProductCommand
        {
            Title = TestTitle
        };

        var result = source.Adapt<Product>();
        result.Title.Title.Should().Be(TestTitle);
    }

    [Fact]
    public void WHEN_Title_is_specified_on_UpdateProductCommand_THEN_Product_should_have_Title_specified()
    {
        var source = new UpdateProductCommand
        {
            Title = TestTitle
        };

        var result = source.Adapt<Product>();
        result.Title.Title.Should().Be(TestTitle);
    }
}