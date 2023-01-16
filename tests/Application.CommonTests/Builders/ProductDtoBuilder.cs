using CleanCompanyName.DDDMicroservice.Application.Products.Dto;

namespace CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;

public static class ProductDtoBuilder
{
    public static ProductDto GetProductDtoEmpty()
    {
        return new ProductDto
        (
            Id: default,
            Sku: default!,
            Title: default!,
            Description: default!,
            Price: default
        );
    }
}

