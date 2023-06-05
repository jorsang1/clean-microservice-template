using CleanCompanyName.DDDMicroservice.Application.Common;
using FluentResults;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CleanCompanyName.DDDMicroservice.Api.Endpoints;

public static class EndpointsExtensions
{
    public static IResult ToHttpResult(
        this Result result,
        Action? actionWhenSuccess = null)
    {
        if (!result.IsSuccess)
            return result.ToHttpError();

        actionWhenSuccess?.Invoke();

        return Results.NoContent();
    }

    public static IResult ToHttpResult<TSource, TDestination>(
        this Result<TSource> result,
        Func<TSource, TDestination> mapping,
        Action? actionWhenSuccess = null)
    {
        if (!result.IsSuccess)
            return result.ToHttpError();

        actionWhenSuccess?.Invoke();

        return result.Value is not null
            ? Results.Ok(mapping(result.Value))
            : Results.NoContent();
    }

    private static IResult ToHttpError(this Result result) => ToBadResult(result.Errors);

    private static IResult ToHttpError<TSource>(this Result<TSource> result) => ToBadResult(result.Errors);

    private static IResult ToBadResult(List<IError> errors)
        => errors.First(e => e.Metadata.ContainsKey("Status")).Metadata["Status"] switch
        {
            ResultStatus.NotFound     => Results.NotFound(errors),
            ResultStatus.Unauthorized => Results.Unauthorized(),
            ResultStatus.Forbidden    => Results.Forbid(),
            ResultStatus.Invalid      => Results.BadRequest(errors),
            ResultStatus.Error        => Results.BadRequest(errors),
            _                         => Results.Problem()
        };
}