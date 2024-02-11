using CleanArchSample.Application.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArchSample.Application.Middlewares
{
    internal class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger) : IMiddleware
    {
        private static readonly string[] SystemError = ["System Error!"];

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Global Error: {ErrorMessage}", ex.Message);
                await HandleException(context, ex);
            }
        }

        private static Task HandleException(HttpContext context, Exception exception)
        {
            var statusCode = GetStatusCode(exception);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            if (exception is ValidationException validationException)
                return context.Response.WriteAsync(new ExceptionModel
                {
                    Errors = validationException.Errors.Select(r => r.ErrorMessage),
                    StatusCode = statusCode
                }.ToString());

            if (exception is BaseRuleException or BaseRepositoryException)
                return context.Response.WriteAsync(new ExceptionModel
                {
                    Errors = new[] { exception.Message },
                    StatusCode = statusCode
                }.ToString());

            return context.Response.WriteAsync(new ExceptionModel
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
