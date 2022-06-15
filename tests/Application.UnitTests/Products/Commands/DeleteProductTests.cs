using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.DeleteProduct;
using CleanCompanyName.DDDMicroservice.Application.UnitTests.Products.ExposedHandlers;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Application.UnitTests.Products.Commands;

public class DeleteProductTests : ProductTestBase
{
    private readonly ILogger<DeleteProductCommandHandlerExposed> _logger;
    private readonly Product _productAllData =
        ProductBuilder
            .Init()
            .WithAllData()
            .Get();

    public DeleteProductTests()
    {
        _logger = new NullLogger<DeleteProductCommandHandlerExposed>();
    }

    [Fact]
    public async Task WHEN_not_providing_valid_Id_THEN_throws_not_found_exception()
    {
        var command = new DeleteProductCommand { Id = Guid.Empty };

        MockSetup
            .SetupRepositoryGetByIdValidResponse
            (
                ProductRepository,
                ProductBuilder
                    .Init()
                    .WithoutData()
                    .Get()
            );

        var requestHandler = new DeleteProductCommandHandlerExposed
        (
            ProductRepository.Object,
            _logger
        );

        await FluentActions
            .Invoking(() =>
                requestHandler.ExposedHandle(command, new CancellationToken()))
            .Should()
            .ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task WHEN_providing_valid_id_THEN_product_is_deleted()
    {
        var command = new DeleteProductCommand { Id = _productAllData.Id.Value };

        MockSetup.SetupRepositoryGetByIdValidResponse(ProductRepository, _productAllData);

        var requestHandler = new DeleteProductCommandHandlerExposed
        (
            ProductRepository.Object,
            _logger
        );


        await FluentActions
            .Invoking(() =>
                requestHandler.ExposedHandle(command, new CancellationToken()))
            .Should()
            .NotThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task WHEN_deleting_product_and_getting_an_error_on_delete_THEN_error_is_logged()
    {
        var command = new DeleteProductCommand { Id = _productAllData.Id.Value };

        var mockLogger = new Mock<ILogger<DeleteProductCommandHandlerExposed>>();

        MockSetup.SetupRepositoryGetByIdValidResponse(ProductRepository, _productAllData);
        MockSetup.SetupRepositoryDeleteErrorResponse(ProductRepository);

        var requestHandler = new DeleteProductCommandHandlerExposed
        (
            ProductRepository.Object,
            mockLogger.Object
        );

        await requestHandler.ExposedHandle(command, CancellationToken.None);

        mockLogger.Verify(l => l.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((value, _) =>
                    value
                        .ToString()!
                        .Contains($"Problem deleting the product {command.Id}")),
                It.IsAny<Exception>(),
                ((Func<It.IsAnyType, Exception, string>)It.IsAny<object>())!),
            Times.Once);
    }
}