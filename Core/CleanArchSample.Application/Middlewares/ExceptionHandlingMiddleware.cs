using CleanArchSample.Application.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArchSample.Application.Middlewares
{
    internal sealed class ExceptionHandlingMiddleware(RequestDelegate next)
    {
        private static readonly string[] SystemError = ["System Error!"];

        public async Task InvokeAsync(HttpContext httpContext, ILogger<ExceptionHandlingMiddleware> logger)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Global Error: {ErrorMessage}", ex.Message);
                await HandleException(httpContext, ex);
            }
        }

        private static Task HandleException(HttpContext httpContext, Exception exception)
        {
            var statusCode = GetStatusCode(exception);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;

            if (exception is ValidationException validationException)
                return httpContext.Response.WriteAsync(new ExceptionModel
                {
                    Errors = validationException.Errors.Select(r => r.ErrorMessage),
                    StatusCode = statusCode
                }.ToString());

            if (exception is BaseRuleException or BaseRepositoryException)
                return httpContext.Response.WriteAsync(new ExceptionModel
                {
                    Errors = new[] { exception.Message },
                    StatusCode = statusCode
                }.ToString());

            return httpContext.Response.WriteAsync(new ExceptionModel
            {
                Errors = SystemError,
                StatusCode = statusCode
            }.ToString());
        }

        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                ValidationException => StatusCodes.Status400BadRequest,
                BaseRuleException => StatusCodes.Status422UnprocessableEntity,
                BaseRepositoryException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };
    }
}
