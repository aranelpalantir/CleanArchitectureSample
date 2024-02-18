﻿using CleanArchSample.SharedKernel;

namespace CleanArchSample.Api.Extensions
{
    public static class ResultExtensions
    {
        public static IResult ToProblemDetails(this Result result)
        {
            if (result.IsSuccess)
                throw new InvalidOperationException();
            return Results.Problem(
                statusCode: GetStatusCode(result.Error.ErrorType),
                title: GetTitle(result.Error.ErrorType),
                type: GetType(result.Error.ErrorType),
                extensions: new Dictionary<string, object?>
                {
                    { "errors", new[] { result.Error } }
                });

            static int GetStatusCode(ErrorType errorType) =>
                errorType switch
                {
                    ErrorType.Validation => StatusCodes.Status400BadRequest,
                    ErrorType.NotFound => StatusCodes.Status404NotFound,
                    ErrorType.Conflict => StatusCodes.Status409Conflict,
                    _ => StatusCodes.Status500InternalServerError
                };

            static string GetTitle(ErrorType errorType) =>
                errorType switch
                {
                    ErrorType.Validation => "Bad Request",
                    ErrorType.NotFound => "Not Found",
                    ErrorType.Conflict => "Conflict",
                    _ => "Server Failure"
                };
            static string GetType(ErrorType errorType) =>
                errorType switch
                {
                    ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                    ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                    _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
                };

        }
    }
}
