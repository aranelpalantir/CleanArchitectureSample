using CleanArchSample.Application.Enums;
using CleanArchSample.Application.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchSample.Api.Infrastructure
{
    internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            SendLog(exception);
            try
            {
                var errorType = GetErrorType(exception);
                logger.LogError(exception, "Global Error: {ErrorMessage}", exception.Message);
                var problemDetails = new ProblemDetails
                {
                    Status = GetStatusCode(errorType),
                    Title = GetTitle(errorType),
                    Type = GetProblemType(errorType),
                    Detail = exception.Message
                };

                var validationErrors = GetValidationErrors(exception);
                if (validationErrors is not null)
                    problemDetails.Extensions.Add("validation_errors", validationErrors);

                static ErrorType GetErrorType(Exception exception) =>
                    exception switch
                    {
                        ValidationException => ErrorType.Validation,
                        BaseBusinessRuleException ruleException => ruleException.ErrorType,
                        BaseRepositoryException repositoryException => repositoryException.ErrorType,
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

                static Dictionary<string, IEnumerable<string>>? GetValidationErrors(Exception exception) =>
                    exception switch
                    {
                        ValidationException validationException => validationException.Errors
                            .GroupBy(error => error.PropertyName)
                            .ToDictionary(group => group.Key,
                                group => group.Select(error => error.ErrorMessage)),
                        _ => default
                    };

                httpContext.Response.StatusCode = problemDetails.Status.Value;

                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

                return true;

            }
            catch (Exception exFatal)
            {
                logger.LogCritical(exFatal, "Fatal Error: {ErrorMessage}", exFatal.Message);
                throw;
            }
        }

        private void SendLog(Exception exception)
        {
            if (exception is ValidationException or BaseBusinessRuleException or BaseRepositoryException)
                logger.LogTrace(exception, "{ExceptionType} {ErrorMessage}", typeof(ValidationException), exception.Message);
            else
                logger.LogError(exception, "Global Unhandled Error: {ErrorMessage}", exception.Message);
        }
    }
}
