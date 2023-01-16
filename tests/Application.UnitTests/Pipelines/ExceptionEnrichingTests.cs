using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Application.Pipelines;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;
using CleanCompanyName.DDDMicroservice.Application.Products.Dto;
using MediatR;

namespace CleanCompanyName.DDDMicroservice.Application.UnitTests.Pipelines;

public class ExceptionEnrichingTests
{
    private readonly Mock<RequestHandlerDelegate<ProductDto>> _handlerMock = new();
    private readonly AddProductCommand _command;
    private readonly ExceptionEnrichingPipelineBehaviour<AddProductCommand, ProductDto> _sut = new();

    public ExceptionEnrichingTests()
    {
        _command = AddProductCommandBuilder.GetAddProductCommandEmpty();
    }

    [Fact]
    public async Task WHEN_handling_error_THEN_ActionName_data_should_be_filled()
    {
        _handlerMock
            .Setup(x => x.Invoke())
            .ThrowsAsync(new Exception());

        await FluentActions
            .Invoking(()  =>
                _sut.Handle(
                    _command,
                    CancellationToken.None,
                    _handlerMock.Object))
            .Should()
            .ThrowAsync<Exception>()
            .Where(x => x.Data.Contains("ActionName"));
    }

    [Fact]
    public async Task WHEN_no_error_happens_THEN_no_exception_should_be_thrown()
    {
        var productDto = ProductDtoBuilder.GetProductDtoEmpty();

        _handlerMock
            .Setup(x => x.Invoke())
            .ReturnsAsync(productDto);

        await FluentActions
            .Invoking(() =>
                _sut.Handle(
                    _command,
                    CancellationToken.None,
                    _handlerMock.Object))
            .Should()
            .NotThrowAsync<Exception>();
    }
}