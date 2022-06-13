using CleanCompanyName.DDDMicroservice.Application.Common.Mappers;

namespace CleanCompanyName.DDDMicroservice.Application.UnitTests;

public class MapperConfigSetup
{
    public MapperConfigSetup()
    {
        MapperConfig.AddMappingConfigs();
    }
}