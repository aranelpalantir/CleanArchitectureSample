namespace CleanArchSample.Application.Exceptions
{
    public class BaseRepositoryException : ApplicationException
    {
        public ErrorType ErrorType { get; init; }
        public BaseRepositoryException() { }

        public BaseRepositoryException(string message, ErrorType errorType = ErrorType.Validation) : base(message)
        {
            ErrorType = errorType;
        }
    }
}
