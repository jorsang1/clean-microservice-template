using CleanCompanyName.DDDMicroservice.Domain.Entities.Products.ValueObjects;

namespace CleanCompanyName.DDDMicroservice.Application.Common.Mappers;

public static class MapperConfig
{
    public static void AddMappingConfigs()
    {
        TypeAdapterConfig<ProductTitle, string>
            .ForType()
            .MapWith(productTitle => productTitle.Value);

        TypeAdapterConfig<string, ProductTitle>
            .ForType()
            .MapWith(title => new ProductTitle(title));

        TypeAdapterConfig<ProductId, Guid>
            .ForType()
            .MapWith(productId => productId.Value);

        TypeAdapterConfig<Guid, ProductId>
            .ForType()
            .MapWith(value => new ProductId(value));
    }
}