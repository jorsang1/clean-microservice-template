using CleanCompanyName.DDDMicroservice.Application.CommonTests.Builders;
using CleanCompanyName.DDDMicroservice.Application.Pipelines;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;
using CleanCompanyName.DDDMicroservice.Application.Products.Dto;
using FluentAssertions;
using FluentAssertions.Specialized;
using Mapster;
using MediatR;
using Moq;
using Xunit;

namespace CleanCompanyName.DDDMicroservice.Application.UnitTests.Pipelines;

public class ExceptionEnrichingTests
{
    [Fact]
    public async Task WHEN_handling_error_THEN_ActionName_data_should_be_filled()
    {
        var product = ProductBuilder.GetProduct();
        var command = product.Adapt<AddProductCommand>();

        var mockRequestHandlerDelegate = new Mock<RequestHandlerDelegate<ProductDto>>();

        mockRequestHandlerDelegate
            .Setup(x => x.Invoke())
            .ThrowsAsync(new Exception());

        var requestHandler = new ExceptionEnrichingPipelineBehaviour<AddProductCommand, ProductDto>();

        await FluentActions.Invoking(()  =>
                requestHandler.Handle(
                    command,
                    CancellationToken.None,
                    mockRequestHandlerDelegate.Object))
            .Should()
            .ThrowAsync<Exception>()
            .Where(x => x.Data.Contains("ActionName"));
    }

    [Fact]
    public async Task WHEN_no_error_happens_THEN_no_exception_should_be_thrown()
    {
        var product = ProductBuilder.GetProduct();
        var command = product.Adapt<AddProductCommand>();
        var productDto = product.Adapt<ProductDto>();

        var mockRequestHandlerDelegate = new Mock<RequestHandlerDelegate<ProductDto>>();

        mockRequestHandlerDelegate
            .Setup(x => x.Invoke())
            .ReturnsAsync(productDto);

        var requestHandler = new ExceptionEnrichingPipelineBehaviour<AddProductCommand, ProductDto>();

        await FluentActions.Invoking(() =>
                requestHandler.Handle(
                    command,
                    CancellationToken.None,
                    mockRequestHandlerDelegate.Object))
            .Should()
            .NotThrowAsync<Exception>();
    }
}