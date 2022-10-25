using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.UpdateProduct;
using CleanCompanyName.DDDMicroservice.Domain.Common.Exceptions;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.Logging;

namespace CleanCompanyName.DDDMicroservice.Application.UnitTests.Products.Commands;

public class UpdateProductTests : ProductTestBase
{
    private readonly Mock<IDateTimeService> _dateTimeService = new();
    private readonly Mock<ILogger<UpdateProductCommandHandler>> _logger = new();
    private readonly Mock<IValidator<Product>> _validator = new();
    private readonly UpdateProductCommandHandler _sut;

    public UpdateProductTests()
    {
        _sut = new UpdateProductCommandHandler
        (
            ProductRepository.Object,
            _dateTimeService.Object,
            _logger.Object,
            _validator.Object
        );
    }

    // [Fact]
    // public async Task WHEN_few_fields_are_filled_THEN_throws_validation_exception()
    // {
    //     MockSetup.SetupValidationErrorResponse(_validator);
    //
    //     await FluentActions
    //         .Invoking(() =>
    //             _sut.Handle(ProductBuilder.GetProductWithSku().Adapt<UpdateProductCommand>(),
    //                 CancellationToken.None))
    //         .Should()
    //         .ThrowAsync<DomainValidationException>();
    // }

    [Fact]
    public async Task WHEN_all_fields_are_filled_THEN_product_is_updated()
    {
        var product = ProductBuilder.GetProduct();
        var command = product.Adapt<UpdateProductCommand>();
        MockSetup.SetupValidationValidResponse(_validator);
        MockSetup.SetupRepositoryGetByIdValidResponse(ProductRepository, product);

        var result = await _sut.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        var productDto = result!.Value;

        productDto.Sku.Should().Be(command.Sku);
        productDto.Title.Should().Be(command.Title);
    }

    [Fact]
    public async Task WHEN_product_is_not_found_THEN_product_is_not_returned()
    {
        var product = ProductBuilder.GetProductEmpty();
        var command = product.Adapt<UpdateProductCommand>();
        MockSetup.SetupValidationValidResponse(_validator);
        MockSetup.SetupRepositoryGetByIdNullResponse(ProductRepository);

        var result = await _sut.Handle(command, CancellationToken.None);

        result.Should().BeNull();
    }

    [Fact]
    public async Task WHEN_updating_product_and_getting_an_error_on_update_THEN_error_is_logged()
    {
        var product = ProductBuilder.GetProduct();
        var command = product.Adapt<UpdateProductCommand>();
        MockSetup.SetupValidationValidResponse(_validator);
        MockSetup.SetupRepositoryGetByIdValidResponse(ProductRepository, product);
        MockSetup.SetupRepositoryUpdateErrorResponse(ProductRepository);

        await _sut.Handle(command, CancellationToken.None);

        _logger.Verify(l => l.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((value, _) =>
                    value
                        .ToString()!
                        .Contains(
                            $"Problem updating the stock of the product on the stock service for the product {command.Id}, {command.Title}")),
                It.IsAny<Exception>(),
                ((Func<It.IsAnyType, Exception, string>)It.IsAny<object>())!),
            Times.Once);
    }
}