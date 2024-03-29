﻿using CleanCompanyName.DDDMicroservice.Api.Contracts.Requests;
using CleanCompanyName.DDDMicroservice.Api.Contracts.Responses;
using CleanCompanyName.DDDMicroservice.Api.Telemetry;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.AddProduct;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.DeleteProduct;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.UpdateProduct;
using CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetAllProducts;
using CleanCompanyName.DDDMicroservice.Application.Products.Queries.GetProduct;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanCompanyName.DDDMicroservice.Api.Endpoints;

internal class ProductEndpoints
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/products", GetAllProducts)
            .WithName("GetAllProducts");

        app.MapGet("/products/{id:guid}", GetProduct)
            .WithName("GetProduct");

        app.MapPost("/products", AddProduct)
            .WithName("AddProduct");

        app.MapPut("/products", UpdateProduct)
            .WithName("UpdateProduct");

        app.MapDelete("/products/{id:guid}", DeleteProduct)
            .WithName("DeleteProduct");
    }

    private static async Task<IResult> GetAllProducts(IMediator mediator, ILogger<ProductEndpoints> logger, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all products. (Just an example on how to log inside a 'controller' if you need it)");
        var products = await mediator.Send(new GetAllProductsQuery(), cancellationToken);
        return Results.Ok(products.Adapt<List<ProductListItemResponse>>());
    }

    private static async Task<IResult> GetProduct(Guid id, IMediator mediator, CancellationToken cancellationToken)
    {
        var product = await mediator.Send(new GetProductQuery(id), cancellationToken);

        return
            product is not null
                ? Results.Ok(product.Adapt<ProductResponse>())
                : Results.NotFound();
    }

    private static async Task<IResult> AddProduct([FromBody] AddProductRequest request, IMediator mediator, ILogger<ProductEndpoints> logger, CancellationToken cancellationToken)
    {
        var command = new AddProductCommand(
            Id: request.Id,
            Sku: request.Sku,
            Title: request.Sku,
            Description: request.Description,
            Price: request.Price,
            UserId: Guid.NewGuid());  // TODO replace with proper user identity

        var addProductResult = await mediator.Send(command, cancellationToken);

        return addProductResult.ToHttpResult(
            mapping: obj => obj.Adapt<ProductResponse>(),
            actionWhenSuccess: ApplicationMetrics.NewProductAdded);
    }

    private static async Task<IResult> UpdateProduct([FromBody] UpdateProductRequest request, IMediator mediator, ILogger<ProductEndpoints> logger, CancellationToken cancellationToken)
    {
        var command = new UpdateProductCommand(
            Id: request.Id,
            Sku: request.Sku,
            Title: request.Sku,
            Description: request.Description,
            Price: request.Price,
            UserId: Guid.NewGuid());  // TODO replace with proper user identity

        var updateProductResult = await mediator.Send(command, cancellationToken);

        return updateProductResult.ToHttpResult(mapping: obj => obj.Adapt<ProductResponse>());
    }

    private static async Task<IResult> DeleteProduct(Guid id, IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new DeleteProductCommand(id);
        var deleteProductResult = await mediator.Send(command, cancellationToken);
        return deleteProductResult.ToHttpResult();
    }
}