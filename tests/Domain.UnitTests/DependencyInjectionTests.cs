using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Domain.UnitTests;

public class DependencyInjectionTests
{
    private readonly IServiceCollection _services = new ServiceCollection();

    [Fact]
    public void WHEN_not_registering_validators_THEN_get_an_error()
    {
        var provider = _services.BuildServiceProvider();

        FluentActions.Invoking(() =>
                provider.GetRequiredService<IValidator<Product>>())
            .Should()
            .Throw<InvalidOperationException>();
    }

    [Fact]
    public void WHEN_registering_validators_THEN_can_get_the_service()
    {
        _services.AddDomain();
        var provider = _services.BuildServiceProvider();

        var resultService = provider.GetRequiredService<IValidator<Product>>();

        resultService
            .Should()
            .BeOfType<ProductValidator>();
    }
}