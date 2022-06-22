using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products.ValueObjects;
using CleanCompanyName.DDDMicroservice.Infrastructure.Database.Models;
using Mapster;

namespace CleanCompanyName.DDDMicroservice.Infrastructure.Common.Mappers;


public static class InfrastructureMapperConfig
{
    public static void AddMappingConfigs()
    {
        TypeAdapterConfig<string, ProductTitle>
            .ForType()
            .MapWith(title => new ProductTitle(title));

        TypeAdapterConfig<Guid, ProductId>
            .ForType()
            .MapWith(value => new ProductId(value));

        TypeAdapterConfig<ProductModel, Product>
            .ForType()
            .MapToConstructor(true);
    }
}