using CleanCompanyName.DDDMicroservice.Application.Common;
using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.Logging;

namespace CleanCompanyName.DDDMicroservice.Application.UnitTests.Products.Commands;

public class AddProductTests : ProductTestBase
{
    private readonly Mock<IDateTimeService> _dateTimeService = new();
    private readonly Mock<ILogger<AddProductCommandHandler>> _logger = new();
    private readonly Mock<IStockClient> _stockClient = new();
    private readonly Mock<IValidator<Product>> _validator = new();
    private readonly AddProductCommandHandler _sut;

    public AddProductTests()
    {
        _sut = new AddProductCommandHandler
        (
            ProductRepository.Object,
            _dateTimeService.Object,
            _stockClient.Object,
            _logger.Object,
            _validator.Object
        );
    }

    [Fact]
    public async Task WHEN_no_fields_are_filled_THEN_result_should_contain_the_error_with_message_and_status_invalid()
    {
        var product = ProductBuilder.GetProductEmpty();
        var command = product.Adapt<AddProductCommand>();
        MockSetup.SetupValidationErrorResponse(_validator);

        var result = await _sut.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.First().Message.Should().Be("error");
        result.Errors.First().Metadata["Status"].Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public async Task WHEN_only_Sku_is_filled_THEN_result_should_contain_the_error()
    {
        var product = ProductBuilder.GetProductWithSku();
        var command = product.Adapt<AddProductCommand>();
        MockSetup.SetupValidationErrorResponse(_validator);

        var result = await _sut.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.First().Message.Should().Be("error");
        result.Errors.First().Metadata["Status"].Should().Be(ResultStatus.Invalid);
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
        result.IsSuccess.Should().BeTrue();
        result.Value.Sku.Should().Be(command.Sku);
        result.Value.Title.Should().Be(command.Title);
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
                    .Contains($"Error updating the stock for the product {result.Value.Id}")),
            It.IsAny<HttpRequestException>(),
            ((Func<It.IsAnyType, Exception, string>)It.IsAny<object>())!),
            Times.Once);
    }
}