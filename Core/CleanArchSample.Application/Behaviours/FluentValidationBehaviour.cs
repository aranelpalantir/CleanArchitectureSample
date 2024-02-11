using FluentValidation;
using MediatR;

namespace CleanArchSample.Application.Behaviours
{
    public class FluentValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = validators
                .Select(r => r.Validate(context))
                .SelectMany(r => r.Errors)
                .GroupBy(r => r.ErrorMessage)
                .Select(r => r.First())
                .Where(r => r != null)
                .ToList();
            if (failures.Count != 0)
                throw new ValidationException(failures);
            return await next();
        }
    }
}
