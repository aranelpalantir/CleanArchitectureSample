namespace CleanArchSample.Application.Exceptions
{
    public class BaseRepositoryException : ApplicationException
    {
        public BaseRepositoryException() { }

        public BaseRepositoryException(string message) : base(message) { }
    }
}
