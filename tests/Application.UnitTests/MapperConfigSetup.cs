using CleanCompanyName.DDDMicroservice.Application.Common.Mappers;

namespace CleanCompanyName.DDDMicroservice.Application.UnitTests;

public class MapperConfigSetup
{
    public MapperConfigSetup()
    {
        ApplicationMapperConfig.AddMappingConfigs();
    }
}