using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;
using CleanCompanyName.DDDMicroservice.Domain.Common.Exceptions;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.Logging;

namespace CleanCompanyName.DDDMicroservice.Application.UnitTests.Products.Commands;

public class AddProductTests : ProductTestBase
{
    private readonly Mock<IDateTime> _dateService = new();
    private readonly Mock<ILogger<AddProductCommandHandler>> _logger = new();
    private readonly Mock<IStockClient> _stockClient = new();
    private readonly Mock<IValidator<Product>> _validator = new();
    private readonly AddProductCommandHandler _sut;

    public AddProductTests()
    {
        _sut = new AddProductCommandHandler
        (
            ProductRepository.Object,
            _dateService.Object,
            _stockClient.Object,
            _logger.Object,
            _validator.Object
        );
    }

    [Fact]
    public async Task WHEN_no_fields_are_filled_THEN_throws_validation_exception()
    {
        MockSetup.SetupValidationErrorResponse(_validator);

        await FluentActions
            .Invoking(() =>
                _sut.Handle(ProductBuilder.GetProductEmpty().Adapt<AddProductCommand>(),
                    CancellationToken.None))
            .Should()
            .ThrowAsync<DomainValidationException>();
    }

    [Fact]
    public async Task WHEN_only_Sku_is_filled_THEN_throws_validation_exception()
    {
        MockSetup.SetupValidationErrorResponse(_validator);

        await FluentActions
            .Invoking(() =>
                _sut.Handle(ProductBuilder.GetProductWithSku().Adapt<AddProductCommand>(),
                    CancellationToken.None))
            .Should()
            .ThrowAsync<DomainValidationException>();
    }

    [Fact]
    public async Task WHEN_all_fields_are_filled_THEN_product_is_created()
    {
        var product = ProductBuilder.GetProduct();
        var command = product.Adapt<AddProductCommand>();
        MockSetup.SetupValidationValidResponse(_validator);
        MockSetup.SetupRepositoryCreateValidResponse(ProductRepository, product);

        var result = await _sut.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result!.Sku.Should().Be(command.Sku);
        result.Title.Should().Be(command.Title);
    }

    [Fact]
    public async Task WHEN_adding_product_without_stockClient_THEN_error_is_logged()
    {
        var product = ProductBuilder.GetProduct();
        var command = product.Adapt<AddProductCommand>();
        MockSetup.SetupValidationValidResponse(_validator);
        MockSetup.SetupRepositoryCreateValidResponse(ProductRepository, product);
        MockSetup.SetupStockClientErrorResponse(_stockClient);

        var result = await _sut.Handle(command, CancellationToken.None);

        _logger.Verify(l => l.Log(
            LogLevel.Error,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((value, _) =>
                value
                    .ToString()!
                    .Contains($"Error updating the stock for the product {result.Id}")),
            It.IsAny<HttpRequestException>(),
            ((Func<It.IsAnyType, Exception, string>)It.IsAny<object>())!),
            Times.Once);
    }
}