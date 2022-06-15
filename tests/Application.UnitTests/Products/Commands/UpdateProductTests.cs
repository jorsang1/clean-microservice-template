using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.UpdateProduct;
using CleanCompanyName.DDDMicroservice.Application.UnitTests.Products.ExposedHandlers;
using CleanCompanyName.DDDMicroservice.Domain.Common.Exceptions;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using FluentAssertions;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Application.UnitTests.Products.Commands;

public class UpdateProductTests : ProductTestBase
{
    private readonly Mock<IDateTime> _dateService;
    private readonly ILogger<UpdateProductCommandHandler> _logger;
    private readonly Mock<IValidator<Product>> _validator;
    private readonly Product _productAllData =
        ProductBuilder
            .Init()
            .WithAllData()
            .Get();

    public UpdateProductTests()
    {
        _dateService = new Mock<IDateTime>();
        _logger = new NullLogger<UpdateProductCommandHandler>();
        _validator = new Mock<IValidator<Product>>();
    }

    [Fact]
    public async Task WHEN_few_fields_are_filled_THEN_throws_validation_exception()
    {
        MockSetup.SetupValidationErrorResponse(_validator);

        var requestHandler = new UpdateProductCommandHandler
        (
            ProductRepository.Object,
            _dateService.Object,
            _logger,
            _validator.Object
        );

        await FluentActions
            .Invoking(() =>
                requestHandler.Handle(
                    ProductBuilder
                        .Init()
                        .WithSku("sku")
                        .Get()
                        .Adapt<UpdateProductCommand>(),
                    CancellationToken.None))
            .Should()
            .ThrowAsync<DomainValidationException>();
    }

    [Fact]
    public async Task WHEN_all_fields_are_filled_THEN_product_is_updated()
    {
        var command = _productAllData.Adapt<UpdateProductCommand>();

        MockSetup.SetupValidationValidResponse(_validator);
        MockSetup.SetupRepositoryGetByIdValidResponse(ProductRepository, _productAllData);

        var requestHandler = new UpdateProductCommandHandler
        (
            ProductRepository.Object,
            _dateService.Object,
            _logger,
            _validator.Object
        );

        var result = await requestHandler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();

        var productDto = result!.Value;

        productDto!.Sku.Should().Be(command.Sku);
        productDto.Title.Should().Be(command.Title);
    }

    [Fact]
    public async Task WHEN_product_is_not_found_THEN_product_is_not_returned()
    {
        var command = _productAllData.Adapt<UpdateProductCommand>();

        MockSetup.SetupValidationValidResponse(_validator);
        MockSetup.SetupRepositoryGetByIdNullResponse(ProductRepository);

        var requestHandler = new UpdateProductCommandHandler
        (
            ProductRepository.Object,
            _dateService.Object,
            _logger,
            _validator.Object
        );

        var result = await requestHandler.Handle(command, CancellationToken.None);

        result.Should().BeNull();
    }

    [Fact]
    public async Task WHEN_updating_product_and_getting_an_error_on_update_THEN_error_is_logged()
    {
        var command = _productAllData.Adapt<UpdateProductCommand>();

        var mockLogger = new Mock<ILogger<UpdateProductCommandHandlerExposed>>();

        MockSetup.SetupValidationValidResponse(_validator);
        MockSetup.SetupRepositoryGetByIdValidResponse(ProductRepository, _productAllData);
        MockSetup.SetupRepositoryUpdateErrorResponse(ProductRepository);

        var requestHandler = new UpdateProductCommandHandlerExposed
        (
            ProductRepository.Object,
            _dateService.Object,
            mockLogger.Object,
            _validator.Object
        );

        var result = await requestHandler.Handle(command, CancellationToken.None);

        mockLogger.Verify(l => l.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((value, _) =>
                    value
                        .ToString()!
                        .Contains($"Problem updating the stock of the product on the stock service for the product {command.Id}, {command.Title}")),
                It.IsAny<Exception>(),
                ((Func<It.IsAnyType, Exception, string>)It.IsAny<object>())!),
            Times.Once);
    }
}