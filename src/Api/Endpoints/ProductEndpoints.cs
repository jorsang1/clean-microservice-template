using CleanCompanyName.DDDMicroservice.Api.Contracts.Requests;
using CleanCompanyName.DDDMicroservice.Api.Contracts.Responses;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.DeleteProduct;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.UpdateProduct;
using CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetAllProducts;
using CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetProduct;
using CleanCompanyName.DDDMicroservice.Domain.Common.Exceptions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanCompanyName.DDDMicroservice.Api.Endpoints;

public class ProductEndpoints
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/products", GetAllProducts)
            .WithName("GetAllProducts");

        app.MapGet("/products/{id}", GetProduct)
            .WithName("GetProduct");

        app.MapPost("/products", AddProduct)
            .WithName("AddProduct");

        app.MapPut("/products", UpdateProduct)
            .WithName("UpdateProduct");

        app.MapDelete("/products/{id}", DeleteProduct)
            .WithName("DeleteProduct");
    }

    private async Task<IResult> GetAllProducts(IMediator mediator, ILogger<ProductEndpoints> logger, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all products. (Just an example on how to log inside a 'controller' if you need it)");
        var products = await mediator.Send(new GetAllProductsQuery(), cancellationToken);
        return Results.Ok(products.Adapt<List<ProductListItemResponse>>());
    }

    private async Task<IResult> GetProduct(Guid id, IMediator mediator, CancellationToken cancellationToken)
    {
        var product = await mediator.Send(new GetProductQuery { ProductId = id }, cancellationToken);
        return product is not null ? Results.Ok(product.Adapt<ProductResponse>()) : Results.NotFound();
    }

    private async Task<IResult> AddProduct([FromBody] AddProductRequest request, IMediator mediator, ILogger<ProductEndpoints> logger, CancellationToken cancellationToken)
    {
        var command = request.Adapt<AddProductCommand>();

        try
        {
            var product = await mediator.Send(command, cancellationToken);
            return Results.Ok(product.Adapt<ProductResponse>());
        }
        catch (DomainValidationException validationException)
        {
            logger.LogInformation( "{ErrorMessage} {Errors}", validationException.Message, validationException.Errors);
            return Results.BadRequest(validationException.Errors);
        }
    }

    private async Task<IResult> UpdateProduct([FromBody] UpdateProductRequest request, IMediator mediator, ILogger<ProductEndpoints> logger, CancellationToken cancellationToken)
    {
        var command = request.Adapt<UpdateProductCommand>();

        try
        {
            var product = await mediator.Send(command, cancellationToken);
            return product is not null ? Results.Ok(product.Adapt<ProductResponse>()) : Results.NotFound();
        }
        catch (DomainValidationException validationException)
        {
            logger.LogInformation("{ErrorMessage} {Errors}", validationException.Message, validationException.Errors);
            return Results.BadRequest(validationException.Errors);
        }
    }

    private async Task<IResult> DeleteProduct(Guid id, IMediator mediator, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteProductCommand { Id = id };
            await mediator.Send(command, cancellationToken);
            return Results.NoContent();
        }
        catch (KeyNotFoundException)
        {
            return Results.NotFound();
        }
    }
}