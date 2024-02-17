using CleanArchSample.Application.Interfaces.Security;

namespace CleanArchSample.Application.IntegrationTests
{
    public class TestUserContext : IUserContext
    {
        public string UserName => "test-user";
    }
}
