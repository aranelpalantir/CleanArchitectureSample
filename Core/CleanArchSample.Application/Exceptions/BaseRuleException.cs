namespace CleanArchSample.Application.Exceptions
{
    internal class BaseRuleException : ApplicationException
    {
        public BaseRuleException() { }

        public BaseRuleException(string message) : base(message) { }
    }
}
