using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.DeleteProduct;
using CleanCompanyName.DDDMicroservice.Application.UnitTests.Products.ExposedHandlers;
using Microsoft.Extensions.Logging;

namespace CleanCompanyName.DDDMicroservice.Application.UnitTests.Products.Commands;

public class DeleteProductTests : ProductTestBase
{
    private readonly Mock<ILogger<DeleteProductCommandHandlerExposed>> _logger = new();
    private readonly DeleteProductCommandHandlerExposed _sut;

    public DeleteProductTests()
    {
        _sut = new DeleteProductCommandHandlerExposed
        (
            ProductRepository.Object,
            _logger.Object
        );
    }

    [Fact]
    public async Task WHEN_not_providing_valid_Id_THEN_throws_not_found_exception()
    {
        var command = new DeleteProductCommand(Guid.Empty);

        MockSetup
            .SetupRepositoryGetByIdValidResponse
            (
                ProductRepository,
                ProductBuilder.GetProductEmpty()
            );

        await FluentActions
            .Invoking(() =>
                _sut.ExposedHandle(command, new CancellationToken()))
            .Should()
            .ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task WHEN_providing_valid_id_THEN_product_is_deleted()
    {
        var product = ProductBuilder.GetProduct();
        var command = new DeleteProductCommand(product.Id.Value);

        MockSetup.SetupRepositoryGetByIdValidResponse(ProductRepository, product);

        await FluentActions
            .Invoking(() =>
                _sut.ExposedHandle(command, new CancellationToken()))
            .Should()
            .NotThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task WHEN_deleting_product_and_getting_an_error_on_delete_THEN_error_is_logged()
    {
        var product = ProductBuilder.GetProduct();
        var command = new DeleteProductCommand(product.Id.Value);

        MockSetup.SetupRepositoryGetByIdValidResponse(ProductRepository, product);
        MockSetup.SetupRepositoryDeleteErrorResponse(ProductRepository);

        await _sut.ExposedHandle(command, CancellationToken.None);

        _logger.Verify(l => l.Log(
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