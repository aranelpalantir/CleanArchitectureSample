namespace CleanArchSample.Application.Exceptions
{
    public class BaseRuleException : ApplicationException
    {
        public BaseRuleException()
        {

        }

        public BaseRuleException(string message) : base(message)
        {

        }
    }
}
