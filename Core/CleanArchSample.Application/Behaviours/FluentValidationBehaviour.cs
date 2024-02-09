using FluentValidation;
using MediatR;

namespace CleanArchSample.Application.Behaviours
{
    public class FluentValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public FluentValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(r => r.Validate(context))
                .SelectMany(r => r.Errors)
                .GroupBy(r => r.ErrorMessage)
                .Select(r => r.First())
                .Where(r => r != null)
                .ToList();
            if (failures.Any())
                throw new ValidationException(failures);
            return await next();
        }
    }
}
