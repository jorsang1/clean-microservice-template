using CleanCompanyName.DDDMicroservice.Domain.Entities.Products.ValueObjects;

namespace CleanCompanyName.DDDMicroservice.Application.Common.Mappers;

public static class ApplicationMapperConfig
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