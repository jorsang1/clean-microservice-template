using CleanCompanyName.DDDMicroservice.Application.Common.Mappers;

namespace CleanCompanyName.DDDMicroservice.Application.UnitTests;

public class DependencyInjectionFixture
{
    public DependencyInjectionFixture()
    {
        MapperConfig.AddMappingConfigs();
    }
}