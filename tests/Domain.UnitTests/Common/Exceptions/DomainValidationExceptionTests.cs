using System.Collections.Generic;
using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Domain.Common.Exceptions;
using CleanCompanyName.DDDMicroservice.Domain.Common.Validators;
using FluentAssertions;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Domain.UnitTests.Common.Exceptions;

public class DomainValidationExceptionTests
{
    [Fact]
    public void WHEN_no_errors_happens_THEN_default_message_is_given()
    {
        var expectedMessage = "Some problems has been found during validation process:";
        var result = new DomainValidationException(new List<ValidationError>());

        result.Should().NotBeNull();
        result.Message.Should().Be(expectedMessage);
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void WHEN_giving_errors_THEN_the_erros_are_available()
    {
        var errors = ProductBuilder.GetValidationErrors();
        var result = new DomainValidationException(errors);

        result.Should().NotBeNull();
        result.Errors.Should().BeSameAs(errors);
    }
}