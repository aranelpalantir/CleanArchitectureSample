using CleanArchSample.Application.Enums;

namespace CleanArchSample.Application.Exceptions
{
    public class BaseBusinessRuleException : ApplicationException
    {
        public ErrorType ErrorType { get; init; }
        public BaseBusinessRuleException() { }

        public BaseBusinessRuleException(string message, ErrorType errorType = ErrorType.Validation) :
            base(message)
        {
            ErrorType = errorType;
        }
    }
}
