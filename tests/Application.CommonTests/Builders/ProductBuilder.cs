using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products.ValueObjects;

namespace CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;

public interface IProductDataSelectionStage
{
    IProductGetterStage WithSku(string sku);
    IProductGetterStage WithoutData();
    IProductGetterStage WithAllData();
}

public interface IProductGetterStage
{
    Product Get();
}

public class ProductBuilder :
    IProductDataSelectionStage,
    IProductGetterStage
{
    private Product _product;

    private ProductBuilder() { }

    public static IProductDataSelectionStage Init()
    {
        return new ProductBuilder();
    }

    public IProductGetterStage WithSku(string sku)
    {
        _product = new()
        {
            Sku = sku
        };

        return this;
    }

    public IProductGetterStage WithoutData()
    {
        _product = new();
        return this;
    }

    public IProductGetterStage WithAllData()
    {
        _product = new()
        {
            Id = new ProductId(Guid.NewGuid()),
            Sku = "sku",
            Title = new ProductTitle("title"),
            Description = "Description",
            Price = 5
        };

        return this;
    }

    public Product Get()
    {
        return _product;
    }
}