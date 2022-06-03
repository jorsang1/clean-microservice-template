using System.Threading;
using System.Threading.Tasks;
using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;
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

public class AddProductTests : ProductTestBase
{
    private readonly Mock<IDateTime> _dateService;
    private readonly ILogger<AddProductCommandHandler> _logger;
    private readonly Mock<IStockClient> _stockClient;
    private readonly Mock<IValidator<Product>> _validator;

    public AddProductTests()
    {
        _dateService = new Mock<IDateTime>();
        _stockClient = new Mock<IStockClient>();
        _logger = new NullLogger<AddProductCommandHandler>();
        _validator = new Mock<IValidator<Product>>();
    }

    [Fact]
    public async Task WHEN_no_fields_are_filled_THEN_throws_validation_exception()
    {
        MockSetup.SetupValidationErrorResponse(_validator);

        var requestHandler = new AddProductCommandHandler
        (
            ProductRepository.Object,
            _dateService.Object,
            _stockClient.Object,
            _logger,
            _validator.Object
        );

        await FluentActions
            .Invoking(() =>
                requestHandler.Handle(ProductBuilder.GetProductEmpty().Adapt<AddProductCommand>(),
                    CancellationToken.None))
            .Should()
            .ThrowAsync<DomainValidationException>();
    }

    [Fact]
    public async Task WHEN_few_fields_are_filled_THEN_throws_validation_exception()
    {
        MockSetup.SetupValidationErrorResponse(_validator);

        var requestHandler = new AddProductCommandHandler
        (
            ProductRepository.Object,
            _dateService.Object,
            _stockClient.Object,
            _logger,
            _validator.Object
        );

        await FluentActions
            .Invoking(() =>
                requestHandler.Handle(ProductBuilder.GetProductWithSku().Adapt<AddProductCommand>(),
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

        var requestHandler = new AddProductCommandHandler
        (
            ProductRepository.Object,
            _dateService.Object,
            _stockClient.Object,
            _logger,
            _validator.Object
        );

        var result = await requestHandler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result!.Sku.Should().Be(command.Sku);
        result.Title.Should().Be(command.Title);
    }
}