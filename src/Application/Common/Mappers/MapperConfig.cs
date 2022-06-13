using CleanCompanyName.DDDMicroservice.Domain.Entities.Products.ValueObjects;

namespace CleanCompanyName.DDDMicroservice.Application.Common.Mappers;

public static class MapperConfig
{
    public static void AddMappingConfigs()
    {
        TypeAdapterConfig<ProjectTitle, string>
            .ForType()
            .MapWith(projectTitle => projectTitle.Title);

        TypeAdapterConfig<string, ProjectTitle>
            .ForType()
            .MapWith(title => new ProjectTitle(title));
    }
}