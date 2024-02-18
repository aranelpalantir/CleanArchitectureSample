using CleanArchSample.Application.Exceptions;
using CleanArchSample.SharedKernel;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchSample.Api.Infrastructure
{
    internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, "Global Error: {ErrorMessage}", exception.Message);
            var errorType = GetErrorType(exception);
            var problemDetails = new ProblemDetails
            {
                Status = GetStatusCode(errorType),
                Title = GetTitle(errorType),
                Type = GetProblemType(errorType),
                Extensions = new Dictionary<string, object?>
                {
                    { "errors", new[] { GetErrors(exception) } }
                }
            };

            static ErrorType GetErrorType(Exception exception) =>
                exception switch
                {
                    ValidationException => ErrorType.Validation,
                    BaseBusinessRuleException ruleException => ruleException.ErrorType,
                    BaseRepositoryException => ErrorType.Validation,
                    _ => ErrorType.Failure
                };
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
            static string GetProblemType(ErrorType errorType) =>
                errorType switch
                {
                    ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                    ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                    _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
                };

            static string[] GetErrors(Exception exception) =>
                exception switch
                {
                    ValidationException validationException => validationException.Errors.Select(r => r.ErrorMessage).ToArray(),
                    _ => new[] { exception.Message }
                };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
