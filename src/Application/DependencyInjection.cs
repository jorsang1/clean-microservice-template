using System.Reflection;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.UpdateProduct;
using CleanCompanyName.DDDMicroservice.Application.Products.Dto;
using Microsoft.Extensions.DependencyInjection;

namespace CleanCompanyName.DDDMicroservice.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        AddMappingConfigs();

        return services;
    }

    public static void AddMappingConfigs()
    {
        TypeAdapterConfig<Domain.Entities.Product.Product, ProductListItemDto>.NewConfig()
            .Map(dest => dest.Title,
                src => src.Title.Title);

        TypeAdapterConfig<Domain.Entities.Product.Product, ProductDto>.NewConfig()
            .Map(dest => dest.Title,
                src => src.Title.Title);

        TypeAdapterConfig<AddProductCommand, Domain.Entities.Product.Product>.NewConfig()
            .Map(dest => dest.Title,
                src => new Domain.Entities.Product.ValueObjects.ProjectTitle(src.Title));

        TypeAdapterConfig<UpdateProductCommand, Domain.Entities.Product.Product>.NewConfig()
            .Map(dest => dest.Title,
                src => new Domain.Entities.Product.ValueObjects.ProjectTitle(src.Title));
    }
}