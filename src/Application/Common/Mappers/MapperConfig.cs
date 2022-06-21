using CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.UpdateProduct;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products.ValueObjects;

namespace CleanCompanyName.DDDMicroservice.Application.Common.Mappers;

public static class MapperConfig
{
    public static void AddMappingConfigs()
    {
        TypeAdapterConfig<ProductTitle, string>
            .ForType()
            .MapWith(productTitle => productTitle.Value);

        TypeAdapterConfig<ProductId, Guid>
            .ForType()
            .MapWith(productId => productId.Value);
    }
}